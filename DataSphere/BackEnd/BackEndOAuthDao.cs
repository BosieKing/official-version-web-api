using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.BackEnd;
using Microsoft.EntityFrameworkCore;
using Model.Commons.CoreData;
using Model.Commons.Domain;
using Model.Repositotys.Service;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;
using UtilityToolkit.Utils;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 后台权限管理数据访问实现类
    /// </summary>
    public class BackEndOAuthDao : BaseDao<T_User>, IBackEndOAuthDao
    {
        #region 构造函数
        public BackEndOAuthDao(SqlDbContext dbContext) : base(dbContext)
        {

        }
        #endregion

        #region 判断
        /// <summary>
        /// 判断是不是超级管理员
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<bool> IsSuperManage(string phone = "", long userId = 0)
        {
            // 判断是否是超级管理员
            var isSuperManage = await dbContext.UserRep.IgnoreTenantFilter()
                                        .Where(!phone.IsNullOrEmpty() && userId.Equals(0), p => p.Phone == phone)
                                        .Where(!userId.Equals(0) && phone.IsNullOrEmpty(), p => p.Id == userId)
                                        .Select(p => p.TenantId)
                                        .Join(dbContext.TenantRep, u => u, t => t.Id, (u, t) => t.IsSuperManage)
                                        .AnyAsync(p => p == true);
            return isSuperManage;
        }

        /// <summary>
        /// 担任的租户管理员平台中，电话号码和密码是否匹配
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> PassWordInManageExiste(string phone, string password)
        {
            var userIds = await dbContext.UserRep.IgnoreTenantFilter().Where(p => p.Phone == phone && p.Password == password).Select(p => new { p.Id, p.TenantId }).ToListAsync();
            var keys = userIds.Select(p => p.TenantId.ToString()).ToArray();
            if (RedisMulititionHelper.IsSuperManage(keys))
            {
                return true;
            }
            var result = await dbContext.UserRoleRep.IgnoreTenantFilter().AnyAsync(p => userIds.Select(p => p.Id).Contains(p.UserId));
            return result;

        }

        /// <summary>
        /// 判断有无在任一一个租户平台担任管理员
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<bool> IsManage(string phone)
        {
            var userIds = await dbContext.UserRep.IgnoreTenantFilter().Where(p => p.Phone == phone).Select(p => new { p.Id, p.TenantId }).ToListAsync();
            string[] keys = userIds.Select(p => p.TenantId.ToString()).ToArray();
            if (RedisMulititionHelper.IsSuperManage(keys))
            {
                return true;
            }
            var result = await dbContext.UserRoleRep.IgnoreTenantFilter().AnyAsync(p => userIds.Select(p => p.Id).Contains(p.UserId));
            return result;
        }


        /// <summary>
        /// 判断有在该租户平台有无担任管理员
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<bool> InTenantIsManage(long userId, long tenantId)
        {
            bool result = await dbContext.UserRoleRep.IgnoreTenantFilter().AnyAsync(p => userId == p.UserId && p.TenantId == tenantId);
            return result;
        }
        #endregion

        #region 查询数据
        /// <summary>
        /// 获取上方用户信息
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<dynamic> GetUserInfoById(long id)
        {
            var user = await dbContext.UserRep.Where(p => p.Id == id).Select(p => new
            {
                p.AvatarUrl,
                p.Phone,
                p.Email,
                p.NickName,
                p.TenantId
            }).FirstOrDefaultAsync();
            return user;
        }

        /// <summary>
        /// 根据电话号码获取最近登录租户平台的管理员用户信息
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="userId"></param>
        /// <param name="tenantId"></param>
        /// <remarks>当userId不为0的时候，tenantId也必须不为0</remarks>
        /// <returns></returns>
        public async Task<TokenInfoModel> GetUserInfoByPhone(string phone = "", long userId = 0, long tenantId = 0)
        {
            if (!userId.Equals(0) && tenantId.Equals(0))
            {
                throw new InvalidOperationException();
            }

            // 先找到所有用户id             
            var userIdsQuery = dbContext.UserRep.IgnoreTenantFilter()
                                         .Where(!phone.IsNullOrEmpty() && userId.Equals(0), p => p.Phone == phone)
                                         .Where(!userId.Equals(0) && !tenantId.Equals(0) && phone.IsNullOrEmpty(), p => p.Id == userId && p.TenantId == tenantId)
                                         .GroupJoin(dbContext.UserRoleRep, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                                         .SelectMany(p => p.ur.DefaultIfEmpty(), (p, ur) => new
                                         {
                                             UserId = p.u.Id,
                                             p.u.TenantId,
                                             p.u.LoginTime,
                                             RoleId = ur == null ? 0 : ur.RoleId
                                         })
                                         .Where(p => p.RoleId != 0)
                                         .OrderByDescending(p => p.LoginTime)
                                         .GroupBy(p => new { p.UserId, p.TenantId }).Select(p => new
                                         {
                                             p.Key.UserId,
                                             p.Key.TenantId,
                                             RoleId = p.Select(p => p.RoleId)
                                         }).Take(1);
            // 配置了角色的id          
            var result = await userIdsQuery
                                           .Join(dbContext.TenantRep, p => p.TenantId, t => t.Id, (p, t) => new TokenInfoModel
                                           {
                                               UserId = p.UserId.ToString(),
                                               RoleIds = string.Join(",", p.RoleId),
                                               TenantId = p.TenantId.ToString(),
                                               SchemeName = t.Code
                                           }).FirstOrDefaultAsync();
            return result;
        }

        /// <summary>
        /// 获取超管个人信息
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="userId"></param>
        /// <param name="tenantId"></param>
        /// <remarks>当userId不为0的时候，tenantId也必须不为0</remarks>
        /// <returns></returns>
        public async Task<TokenInfoModel> GetSuperManageUserInfoByPhone(string phone = "", long userId = 0, long tenantId = 0)
        {
            if (!userId.Equals(0) && tenantId.Equals(0))
            {
                throw new InvalidOperationException();
            }
            // 先找到所有用户id             
            var userIdsQuery = dbContext.UserRep.IgnoreTenantFilter()
                                         .Where(!phone.IsNullOrEmpty() && userId.Equals(0), p => p.Phone == phone)
                                         .Where(!userId.Equals(0) && !tenantId.Equals(0) && phone.IsNullOrEmpty(), p => p.Id == userId && p.TenantId == tenantId)
                                         .OrderByDescending(p => p.LoginTime)
                                         .Select(p => new
                                         {
                                             UserId = p.Id,
                                             p.TenantId,
                                         }).Take(1);
            var result = await userIdsQuery
                                           .Join(dbContext.TenantRep, p => p.TenantId, t => t.Id, (p, t) => new TokenInfoModel
                                           {
                                               UserId = p.UserId.ToString(),
                                               RoleIds = "",
                                               TenantId = p.TenantId.ToString(),
                                               SchemeName = t.Code
                                           }).FirstOrDefaultAsync();

            return result;
        }


        /// <summary>
        /// 获取电话号码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<string> GetPhoneById(long id)
        {
            var phone = await dbContext.UserRep.Where(p => p.Id == id).Select(p => p.Phone).FirstOrDefaultAsync();
            return phone;
        }

        /// <summary>
        /// 获取菜单权限树
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task<List<MenuTreeModel>> GetMenuTree(long[] roleIds)
        {
            var menuIdsQuery = dbContext.RoleMenuRep.Where(p => roleIds.Contains(p.RoleId)).GroupBy(p => p.MenuId).Select(p => p.Key);
            var list = dbContext.TenantMenuRep.Where(p => menuIdsQuery.Contains(p.Id))
                                       .Select(p => new MenuTreeModel
                                       {
                                           Icon = p.Icon,
                                           Name = p.Name,
                                           Component = Convert.ToString(p.Component),
                                           Router = Convert.ToString(p.ControllerRouter),
                                           Id = p.Id,
                                           BrowserPath = Convert.ToString(p.BrowserPath),
                                           PId = p.DirectoryId,
                                           Type = (int)MenuTreeTypeEnum.Menu,
                                           IsHidden = p.IsHidden
                                       }).Union(
                      dbContext.TenantDirectoryRep.Where(p => dbContext.TenantMenuRep.Where(p => menuIdsQuery.Contains(p.Id)).Select(p => p.DirectoryId).Contains(p.Id))
                                       .Select(p => new MenuTreeModel
                                       {
                                           Icon = p.Icon,
                                           Name = p.Name,
                                           Component = "/",
                                           Router = "/",
                                           BrowserPath = Convert.ToString(p.BrowserPath),
                                           Id = p.Id,
                                           PId = 0,
                                           Type = (int)MenuTreeTypeEnum.Directory,
                                           IsHidden = true
                                       }));
            return await list.ToListAsync();
        }


        /// <summary>
        /// 超管账号获取菜单权限树
        /// </summary>
        /// <param name="rolds"></param>
        /// <returns></returns>
        public async Task<List<MenuTreeModel>> GetSuperManageMenuTree()
        {
            // 所有为系统菜单的数据
            var list = dbContext.MenuRep.Where(p => p.Weight == (int)MenuWeightTypeEnum.SystemManage)
                                       .Select(p => new MenuTreeModel
                                       {
                                           Icon = p.Icon,
                                           Name = p.Name,
                                           Component = Convert.ToString(p.Component),
                                           Router = Convert.ToString(p.ControllerRouter),
                                           BrowserPath = Convert.ToString(p.BrowserPath),
                                           Id = p.Id,
                                           PId = p.DirectoryId,
                                           Type = (int)MenuTreeTypeEnum.Menu,
                                           IsHidden = p.IsHidden
                                       }).Union(
                      dbContext.DirectoryRep.Where(p => dbContext.MenuRep.Where(p => p.Weight == (int)MenuWeightTypeEnum.SystemManage).Select(p => p.DirectoryId).Contains(p.Id))
                                       .Select(p => new MenuTreeModel
                                       {
                                           Icon = p.Icon,
                                           Name = p.Name,
                                           Component = "/",
                                           Router = "/",
                                           BrowserPath = Convert.ToString(p.BrowserPath),
                                           Id = p.Id,
                                           PId = 0,
                                           Type = (int)MenuTreeTypeEnum.Directory,
                                           IsHidden = false
                                       }));
            return await list.ToListAsync();
        }

        /// <summary>
        /// 返回按钮集合
        /// </summary> 
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task<string[]> GetButtonArray(long[] roleIds)
        {
            var menuIdsQuery = dbContext.RoleMenuRep.Where(p => roleIds.Contains(p.RoleId)).GroupBy(p => p.MenuId).Select(p => p.Key);
            var list = await dbContext.TenantMenuButtonRep.Where(p => menuIdsQuery.Contains(p.MenuId))
                                          .GroupJoin(dbContext.TenantMenuRep, tm => tm.MenuId, m => m.Id, (tm, m) => new { tm, m })
                                          .SelectMany(p => p.m.DefaultIfEmpty(), (p, tm) => tm.ControllerRouter + ":" + p.tm.ActionName).ToArrayAsync();
            return list;
        }

        /// <summary>
        /// 返回按钮集合
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> GetSuperManageButtonArray()
        {
            var menuIdsQuery = dbContext.MenuRep.Where(p => p.Weight == (int)MenuWeightTypeEnum.SystemManage).Select(p => p.Id);
            var list = await dbContext.MenuButtonRep.Where(p => menuIdsQuery.Contains(p.Id))
                                          .GroupJoin(dbContext.MenuRep, tm => tm.MenuId, m => m.Id, (tm, m) => new { tm, m })
                                          .SelectMany(p => p.m.DefaultIfEmpty(), (p, tm) => tm.ControllerRouter + ":" + p.tm.ActionName).ToArrayAsync();
            return list;
        }

        /// <summary>
        /// 查询已绑定的租户列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DropdownDataResult>> GetBindTenantList()
        {
            List<DropdownDataResult> list = new List<DropdownDataResult>();
            string code = await dbContext.TenantRep.Where(p => p.Id == dbContext.TenantId).Select(p => p.Code).FirstOrDefaultAsync();
            list.Add(new DropdownDataResult() { Id = dbContext.TenantId, Name = code });
            return list;
        }


        /// <summary>
        /// 查询超管已绑定的租户列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DropdownDataResult>> GetSuperManageBindTenantList(long userId)
        {
            var tenantIdsQuery = dbContext.UserRep.IgnoreTenantFilter().Where(p => p.Id == userId)
                                           .Select(p => p.Phone).Take(1);
            var tenantIdIds = dbContext.UserRep.IgnoreTenantFilter().Where(p => tenantIdsQuery.Contains(p.Phone)).Select(p => p.TenantId);
            return await dbContext.TenantRep.Where(p => tenantIdIds.Contains(p.Id)).Select(p => new DropdownDataResult { Id = p.Id, Name = p.Name }).ToListAsync();

        }
        #endregion

        #region 新增
        #endregion

        #region 更新
        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAvatar(string url, long userId)
        {
            T_User user = await dbContext.UserRep.FirstOrDefaultAsync(p => p.Id == userId);
            user.AvatarUrl = url;
            dbContext.UserRep.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="realName"></param>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserInfo(string realName, string email, long userId)
        {
            T_User user = await dbContext.UserRep.FirstOrDefaultAsync(p => p.Id == userId);
            user.NickName = realName;
            user.Email = email;
            dbContext.UserRep.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassword(string newPassword, long userId)
        {
            T_User user = await dbContext.UserRep.FirstOrDefaultAsync(p => p.Id == userId);
            user.Password = newPassword;
            dbContext.UserRep.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }
        #endregion

    }
}
