using IDataSphere.Extensions;
using IDataSphere.Interface.BackEnd.TenantManage;
using IDataSphere.Repositoty;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Enums;
using SharedLibrary.Models.DomainModels;
using UtilityToolkit.Utils;
using Yitter.IdGenerator;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 租户数据库访问实现类
    /// </summary>
    public class TenantManageDao : BaseDao<T_Tenant>, ITenantManageDao
    {
        #region 构造函数
        public TenantManageDao(SqlDbContext dMDbContext) : base(dMDbContext)
        {
        }
        #endregion

        #region 判断
        /// <summary>
        /// 判断租户编号是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CodeExist(string code, long id = 0)
        {
            var data = await dMDbContext.TenantRep.Where(!code.IsNullOrEmpty() && id.Equals(0), p => p.Code == code)
                                         .Where(!code.IsNullOrEmpty() && !id.Equals(0), p => p.Id != id && p.Code == code).AnyAsync();
            return data;
        }
        #endregion

        #region 查询数据
        /// <summary>
        /// 获取租户已配置的菜单
        /// </summary>
        /// <param name="tenandId"></param>
        /// <returns></returns>
        public async Task<List<DropdownSelectionModel>> GetTenantMenuList(long tenandId)
        {
            var checkList = await dMDbContext.TenantMenuRep.IgnoreTenantFilter().Where(t => t.TenantId == tenandId)
                                             .Select(p => p.UniqueNumber).ToListAsync();
            var list = await dMDbContext.MenuRep.Where(p => p.Weight == (int)MenuWeightTypeEnum.Service || p.Weight == (int)MenuWeightTypeEnum.Customization)
                                       .Select(p => new DropdownSelectionModel
                                       {
                                           IsCheck = checkList.Contains(p.UniqueNumber),
                                           Id = p.Id,
                                           Name = p.Name
                                       }).ToListAsync();
            return list;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PaginationResultModel> GetTenantPage(GetTenantPageInput input)
        {
            var query = from data in dMDbContext.TenantRep
                        .Where(!input.Name.IsNullOrEmpty(), p => EF.Functions.Like(p.Name, $"%{input.Name}%"))
                        .Where(!input.Code.IsNullOrEmpty(), p => EF.Functions.Like(p.Code, $"%{input.Code}%"))
                        join createUser in dMDbContext.UserRep on data.CreatedUserId equals createUser.Id into createUserResult
                        from createUser in createUserResult.DefaultIfEmpty()
                        join updateUser in dMDbContext.UserRep on data.UpdateUserId equals updateUser.Id into updateUserResult
                        from updateUser in updateUserResult.DefaultIfEmpty()
                        select new
                        {
                            data.Id,
                            data.Name,
                            data.Code,
                            data.InviteCode,
                            CreatedName = createUser.NickName,
                            data.CreatedTime,
                            UpdateUserName = updateUser.NickName,
                            data.UpdateTime
                        };
            return await base.AdaptPage(query, input.PageSize, input.PageNo);

        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddTenant(T_Tenant input)
        {
            return await AddAsync(input);
        }

        /// <summary>
        /// 新增租户菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddTenantMenu(long tenantId)
        {
            var menuQuery = dMDbContext.Set<T_Menu>().Where(p => p.Weight == (int)MenuWeightTypeEnum.Service)
                                           .Select(p => new { p.DirectoryId, MenuId = p.Id }).ToList();
            var dirIds = menuQuery.GroupBy(p => p.DirectoryId).Select(p => p.Key);
            var dbDirectoryList = await dMDbContext.DirectoryRep.Where(p => dirIds.Contains(p.Id)).ToListAsync();
            var dbMenuList = await dMDbContext.MenuRep.Where(p => p.Weight == (int)MenuWeightTypeEnum.Service).ToListAsync();
            var dbButtonList = await dMDbContext.MenuButtonRep.Where(p => menuQuery.Select(p => p.MenuId).Contains(p.MenuId)).ToListAsync();
            List<T_TenantDirectory> tenantDirectorieList = new List<T_TenantDirectory>();
            List<T_TenantMenu> tenantMenuList = new List<T_TenantMenu>();
            List<T_TenantMenuButton> tenantMenuButtonList = new List<T_TenantMenuButton>();
            foreach (var dir in dbDirectoryList)
            {
                T_TenantDirectory tenantDirectory = dir.Adapt<T_TenantDirectory>();
                tenantDirectory.Id = YitIdHelper.NextId();
                tenantDirectory.TenantId = tenantId;
                tenantDirectorieList.Add(tenantDirectory);
                var menuList = dbMenuList.Where(p => p.DirectoryId == dir.Id);
                foreach (var menu in menuList)
                {
                    T_TenantMenu tenantMenu = menu.Adapt<T_TenantMenu>();
                    tenantMenu.Id = YitIdHelper.NextId();
                    tenantMenu.TenantId = tenantId;
                    tenantMenu.DirectoryId = tenantDirectory.Id;
                    var buttonlist = dbButtonList.Where(p => p.MenuId == menu.Id).Select(p => p.Adapt<T_TenantMenuButton>()).ToList();
                    buttonlist.ForEach(p =>
                    {
                        p.Id = YitIdHelper.NextId();
                        p.TenantId = tenantId;
                        p.MenuId = tenantMenu.Id;
                    });
                    tenantMenuButtonList.AddRange(buttonlist);
                    tenantMenuList.Add(tenantMenu);
                }
            }
            await dMDbContext.TenantDirectoryRep.AddRangeAsync(tenantDirectorieList);
            await dMDbContext.TenantMenuRep.AddRangeAsync(tenantMenuList);
            await dMDbContext.TenantMenuButtonRep.AddRangeAsync(tenantMenuButtonList);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 新增租户菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> PushTenantMenu(long menuId, long directoryId, long tenantId)
        {
            var menu = await dMDbContext.Set<T_Menu>().Where(p => p.Id == menuId && p.Weight == (int)MenuWeightTypeEnum.Service)
                                           .FirstOrDefaultAsync();
            T_TenantMenu tenantMenu = menu.Adapt<T_TenantMenu>();
            tenantMenu.Id = YitIdHelper.NextId();
            tenantMenu.TenantId = tenantId;
            tenantMenu.DirectoryId = directoryId;
            await base.AddAsync(tenantMenu);
            return true;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新邀请码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inviteCode"></param>
        /// <returns></returns>
        public async Task<bool> UptateInviteCode(long id, string inviteCode)
        {
            var data = await dMDbContext.TenantRep.FirstOrDefaultAsync(p => p.Id == id);
            data.InviteCode = inviteCode;
            dMDbContext.TenantRep.Update(data);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 修改租户信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateTenant(string name, string code, long Id)
        {
            var data = await dMDbContext.TenantRep.FirstOrDefaultAsync(p => p.Id == Id);
            data.Name = name;
            data.Code = code;
            dMDbContext.TenantRep.Update(data);
            await dMDbContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
