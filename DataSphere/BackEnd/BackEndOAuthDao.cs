using IDataSphere.Extensions;
using IDataSphere.Interface.BackEnd;
using IDataSphere.Repositoty;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Enums;
using SharedLibrary.Models.CoreDataModels;
using SharedLibrary.Models.DomainModels;
using UtilityToolkit.Helpers;
using UtilityToolkit.Utils;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 后台验证登录权限
    /// </summary>
    public class BackEndOAuthDao : BaseDao<T_User>, IBackEndOAuthDao
    {
        #region 构造函数
        public BackEndOAuthDao(SqlDbContext dMDbContext) : base(dMDbContext)
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
            var isSuperManage = await dMDbContext.UserRep.IgnoreTenantFilter()
                                        .Where(!phone.IsNullOrEmpty() && userId.Equals(0), p => p.Phone == phone)
                                        .Where(!userId.Equals(0) && phone.IsNullOrEmpty(), p => p.Id == userId)
                                        .Select(p => p.TenantId)
                                       .Join(dMDbContext.TenantRep, u => u, t => t.Id, (u, t) => t.IsSuperManage)
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
            var userIds = await dMDbContext.UserRep.IgnoreTenantFilter().Where(p => p.Phone == phone && p.Password == password).Select(p => new { p.Id, p.TenantId }).ToListAsync();
            var keys = userIds.Select(p => p.TenantId.ToString()).ToArray();
            if (RedisMulititionHelper.IsSuperManage(keys))
            {
                return true;
            }
            var result = await dMDbContext.UserRoleRep.IgnoreTenantFilter().AnyAsync(p => userIds.Select(p => p.Id).Contains(p.UserId));
            return result;

        }

        /// <summary>
        /// 判断有无在任一一个租户平台担任管理员
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<bool> IsManage(string phone)
        {
            var userIds = await dMDbContext.UserRep.IgnoreTenantFilter().Where(p => p.Phone == phone).Select(p => new { p.Id, p.TenantId }).ToListAsync();
            var keys = userIds.Select(p => p.TenantId.ToString()).ToArray();
            if (RedisMulititionHelper.IsSuperManage(keys))
            {
                return true;
            }
            var result = await dMDbContext.UserRoleRep.IgnoreTenantFilter().AnyAsync(p => userIds.Select(p => p.Id).Contains(p.UserId));
            return result;
        }


        /// <summary>
        /// 判断有在该租户平台有无担任管理员
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<bool> InTenantIsManage(long uniqueNumber, long tenantId)
        {
            var userIds = dMDbContext.UserRep.IgnoreTenantFilter().Where(p => p.UniqueNumber == uniqueNumber).Select(p => p.Id);
            var result = await dMDbContext.UserRoleRep.IgnoreTenantFilter().AnyAsync(p => userIds.Contains(p.UserId) && p.TenantId == tenantId);
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
            var user = await dMDbContext.UserRep.Where(p => p.Id == id).Select(p => new
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
            var userIdsQuery = dMDbContext.UserRep.IgnoreTenantFilter()
                                         .Where(!phone.IsNullOrEmpty() && userId.Equals(0), p => p.Phone == phone)
                                         .Where(!userId.Equals(0) && !tenantId.Equals(0) && phone.IsNullOrEmpty(), p => p.Id == userId && p.TenantId == tenantId)
                                         .GroupJoin(dMDbContext.UserRoleRep, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                                         .SelectMany(p => p.ur.DefaultIfEmpty(), (p, ur) => new
                                         {
                                             UserId = p.u.Id,
                                             p.u.TenantId,
                                             p.u.UniqueNumber,
                                             p.u.LoginTime,
                                             RoleId = ur == null ? 0 : ur.RoleId
                                         })
                                         .Where(p => p.RoleId != 0)
                                         .OrderByDescending(p => p.LoginTime)
                                         .GroupBy(p => new { p.UserId, p.TenantId, p.UniqueNumber }).Select(p => new
                                         {
                                             p.Key.UserId,
                                             p.Key.UniqueNumber,
                                             p.Key.TenantId,
                                             RoleId = p.Select(p => p.RoleId)
                                         }).Take(1);
            // 配置了角色的id
            // var userroleIdsQuery = dMDbContext.UserRoleRep.Where(p => userIdsQuery.Contains(p.UserId)).Select(p => new { UserId = p.UserId , RoleId = p.RoleId }).ToList();
            var result = await userIdsQuery
                                           .Join(dMDbContext.TenantRep, p => p.TenantId, t => t.Id, (p, t) => new TokenInfoModel
                                           {
                                               UserId = p.UserId.ToString(),
                                               UniqueNumber = p.UniqueNumber.ToString(),
                                               RoleId = string.Join(",", p.RoleId),
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
            var userIdsQuery = dMDbContext.UserRep.IgnoreTenantFilter()
                                         .Where(!phone.IsNullOrEmpty() && userId.Equals(0), p => p.Phone == phone)
                                         .Where(!userId.Equals(0) && !tenantId.Equals(0) && phone.IsNullOrEmpty(), p => p.Id == userId && p.TenantId == tenantId)
                                         .OrderByDescending(p => p.LoginTime)
                                         .Select(p => new
                                         {
                                             UserId = p.Id,
                                             p.UniqueNumber,
                                             p.TenantId,
                                         }).Take(1);
            var result = await userIdsQuery
                                           .Join(dMDbContext.TenantRep, p => p.TenantId, t => t.Id, (p, t) => new TokenInfoModel
                                           {
                                               UserId = p.UserId.ToString(),
                                               UniqueNumber = p.UniqueNumber.ToString(),
                                               RoleId = "",
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
            var phone = await dMDbContext.UserRep.Where(p => p.Id == id).Select(p => p.Phone).FirstOrDefaultAsync();
            return phone;
        }

        /// <summary>
        /// 获取菜单权限树
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task<List<MenuTreeModel>> GetMenuTree(long[] roleIds)
        {
            var menuIdsQuery = dMDbContext.RoleMenuRep.Where(p => roleIds.Contains(p.RoleId)).GroupBy(p => p.MenuId).Select(p => p.Key);
            var list = dMDbContext.TenantMenuRep.Where(p => menuIdsQuery.Contains(p.Id))
                                       .Select(p => new MenuTreeModel
                                       {
                                           Icon = p.Icon,
                                           Name = p.Name,
                                           Component = Convert.ToString(p.Component),
                                           Router = Convert.ToString(p.Router),
                                           Id = p.Id,
                                           Path = Convert.ToString(p.StrPath),
                                           PId = p.DirectoryId,
                                           Type = (int)MenuTreeTypeEnum.Menu,
                                           IsHidden = p.IsHidden
                                       }).Union(
                      dMDbContext.TenantDirectoryRep.Where(p => dMDbContext.TenantMenuRep.Where(p => menuIdsQuery.Contains(p.Id)).Select(p => p.DirectoryId).Contains(p.Id))
                                       .Select(p => new MenuTreeModel
                                       {
                                           Icon = p.Icon,
                                           Name = p.Name,
                                           Component = "/",
                                           Router = "/",
                                           Path = Convert.ToString(p.StrPath),
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
            var list = dMDbContext.MenuRep.Where(p => p.Weight == (int)MenuWeightTypeEnum.SystemManage)
                                       .Select(p => new MenuTreeModel
                                       {
                                           Icon = p.Icon,
                                           Name = p.Name,
                                           Component = Convert.ToString(p.Component),
                                           Router = Convert.ToString(p.Router),
                                           Path = Convert.ToString(p.StrPath),
                                           Id = p.Id,
                                           PId = p.DirectoryId,
                                           Type = (int)MenuTreeTypeEnum.Menu,
                                           IsHidden = p.IsHidden
                                       }).Union(
                      dMDbContext.DirectoryRep.Where(p => dMDbContext.MenuRep.Where(p => p.Weight == (int)MenuWeightTypeEnum.SystemManage).Select(p => p.DirectoryId).Contains(p.Id))
                                       .Select(p => new MenuTreeModel
                                       {
                                           Icon = p.Icon,
                                           Name = p.Name,
                                           Component = "/",
                                           Router = "/",
                                           Path = Convert.ToString(p.StrPath),
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

            var menuIdsQuery = dMDbContext.RoleMenuRep.Where(p => roleIds.Contains(p.RoleId)).GroupBy(p => p.MenuId).Select(p => p.Key);
            var list = await dMDbContext.TenantMenuButtonRep.Where(p => menuIdsQuery.Contains(p.MenuId))
                                          .GroupJoin(dMDbContext.TenantMenuRep, tm => tm.MenuId, m => m.Id, (tm, m) => new { tm, m })
                                          .SelectMany(p => p.m.DefaultIfEmpty(), (p, tm) => tm.Router + ":" + p.tm.ActionName).ToArrayAsync();
            return list;
        }

        /// <summary>
        /// 返回按钮集合
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> GetSuperManageButtonArray()
        {
            var menuIdsQuery = dMDbContext.MenuRep.Where(p => p.Weight == (int)MenuWeightTypeEnum.SystemManage).Select(p => p.Id);
            var list = await dMDbContext.MenuButtonRep.Where(p => menuIdsQuery.Contains(p.Id))
                                          .GroupJoin(dMDbContext.MenuRep, tm => tm.MenuId, m => m.Id, (tm, m) => new { tm, m })
                                          .SelectMany(p => p.m.DefaultIfEmpty(), (p, tm) => tm.Router + ":" + p.tm.ActionName).ToArrayAsync();
            return list;
        }

        /// <summary>
        /// 查询已绑定的租户列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DropdownDataModel>> GetBindTenantList(long userId)
        {
            var tenantIdsQuery = dMDbContext.UserRep.IgnoreTenantFilter().Where(p => p.Id == userId)
                                           .Select(p => p.Phone).Take(1);
            var userIds = dMDbContext.UserRep.IgnoreTenantFilter().Where(p => tenantIdsQuery.Contains(p.Phone)).Select(p => p.Id);
            var hasRoleTenantIdIds = dMDbContext.UserRoleRep.IgnoreTenantFilter().Where(p => userIds.Contains(p.UserId)).Select(p => p.TenantId);
            return await dMDbContext.TenantRep.Where(p => hasRoleTenantIdIds.Contains(p.Id)).Select(p => new DropdownDataModel { Id = p.Id, Name = p.Name }).ToListAsync();

        }


        /// <summary>
        /// 查询超管已绑定的租户列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DropdownDataModel>> GetSuperManageBindTenantList(long userId)
        {
            var tenantIdsQuery = dMDbContext.UserRep.IgnoreTenantFilter().Where(p => p.Id == userId)
                                           .Select(p => p.Phone).Take(1);
            var tenantIdIds = dMDbContext.UserRep.IgnoreTenantFilter().Where(p => tenantIdsQuery.Contains(p.Phone)).Select(p => p.TenantId);
            return await dMDbContext.TenantRep.Where(p => tenantIdIds.Contains(p.Id)).Select(p => new DropdownDataModel { Id = p.Id, Name = p.Name }).ToListAsync();

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
            var data = await dMDbContext.UserRep.FirstOrDefaultAsync(p => p.Id == userId);
            data.AvatarUrl = url;
            dMDbContext.UserRep.Update(data);
            await dMDbContext.SaveChangesAsync();
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
            var data = await dMDbContext.UserRep.FirstOrDefaultAsync(p => p.Id == userId);
            data.NickName = realName;
            data.Email = email;
            dMDbContext.UserRep.Update(data);
            await dMDbContext.SaveChangesAsync();
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
            var data = await dMDbContext.UserRep.FirstOrDefaultAsync(p => p.Id == userId);
            data.Password = newPassword;
            dMDbContext.UserRep.Update(data);
            await dMDbContext.SaveChangesAsync();
            return true;
        }
        #endregion

    }
}
