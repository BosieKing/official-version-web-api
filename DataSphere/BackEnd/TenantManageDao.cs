using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.BackEnd;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.TenantManage;
using Model.Repositotys;
using SharedLibrary.Enums;
using UtilityToolkit.Utils;
using Yitter.IdGenerator;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 后台租户管理数据访问实现类
    /// </summary>
    public class TenantManageDao : BaseDao, ITenantManageDao
    {
        #region 构造函数
        public TenantManageDao(SqlDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region 查询数据
        /// <summary>
        /// 获取租户已配置的菜单
        /// </summary>
        /// <param name="tenandId"></param>
        /// <returns></returns>
        public async Task<List<DropdownSelectionResult>> GetTenantMenuList(long tenandId)
        {
            List<long> uniqueNumbers = await dbContext.TenantMenuRep.IgnoreTenantFilter().Where(t => t.TenantId == tenandId)
                                             .Select(p => p.UniqueNumber).ToListAsync();
            var list = await dbContext.MenuRep.Where(p => p.Weight == (int)MenuWeightTypeEnum.Service || p.Weight == (int)MenuWeightTypeEnum.Customization)
                                       .Select(p => new DropdownSelectionResult
                                       {
                                           IsCheck = uniqueNumbers.Contains(p.UniqueNumber),
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
        public async Task<PageResult> GetTenantPage(GetTenantPageInput input)
        {
            var query = from tenant in dbContext.TenantRep
                        .Where(!input.Name.IsNullOrEmpty(), p => EF.Functions.Like(p.Name, $"%{input.Name}%"))
                        .Where(!input.Code.IsNullOrEmpty(), p => EF.Functions.Like(p.Code, $"%{input.Code}%"))
                        join createUser in dbContext.UserRep on tenant.CreatedUserId equals createUser.Id into createUserResult
                        from createUser in createUserResult.DefaultIfEmpty()
                        join updateUser in dbContext.UserRep on tenant.UpdateUserId equals updateUser.Id into updateUserResult
                        from updateUser in updateUserResult.DefaultIfEmpty()
                        select new
                        {
                            tenant.Id,
                            tenant.Name,
                            tenant.Code,
                            tenant.InviteCode,
                            CreatedName = createUser.NickName,
                            tenant.CreatedTime,
                            UpdateUserName = updateUser.NickName,
                            tenant.UpdateTime
                        };
            return await base.AdaptPage(query, input.PageSize, input.PageNo);

        }
        #endregion

        #region 新增      

        /// <summary>
        /// 新增租户菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddTenantMenu(long tenantId)
        {
            var menuQuery = dbContext.Set<T_Menu>().Where(p => p.Weight == (int)MenuWeightTypeEnum.Service)
                                                   .Select(p => new { p.DirectoryId, MenuId = p.Id }).ToList();
            IEnumerable<long> dirIds = menuQuery.GroupBy(p => p.DirectoryId).Select(p => p.Key);
            List<T_Directory> dbDirectoryList = await dbContext.DirectoryRep.Where(p => dirIds.Contains(p.Id)).ToListAsync();
            List<T_Menu> dbMenuList = await dbContext.MenuRep.Where(p => p.Weight == (int)MenuWeightTypeEnum.Service).ToListAsync();
            List<T_MenuButton> dbButtonList = await dbContext.MenuButtonRep.Where(p => menuQuery.Select(p => p.MenuId).Contains(p.MenuId)).ToListAsync();
            List<T_TenantDirectory> tenantDirectorieList = new List<T_TenantDirectory>();
            List<T_TenantMenu> tenantMenuList = new List<T_TenantMenu>();
            List<T_TenantMenuButton> tenantMenuButtonList = new List<T_TenantMenuButton>();
            foreach (var dir in dbDirectoryList)
            {
                T_TenantDirectory tenantDirectory = dir.Adapt<T_TenantDirectory>();
                tenantDirectory.Id = YitIdHelper.NextId();
                tenantDirectory.TenantId = tenantId;
                tenantDirectorieList.Add(tenantDirectory);
                IEnumerable<T_Menu> menuList = dbMenuList.Where(p => p.DirectoryId == dir.Id);
                foreach (var menu in menuList)
                {
                    T_TenantMenu tenantMenu = menu.Adapt<T_TenantMenu>();
                    tenantMenu.Id = YitIdHelper.NextId();
                    tenantMenu.TenantId = tenantId;
                    tenantMenu.DirectoryId = tenantDirectory.Id;
                    List<T_TenantMenuButton> buttonlist = dbButtonList.Where(p => p.MenuId == menu.Id).Select(p => p.Adapt<T_TenantMenuButton>()).ToList();
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
            await dbContext.TenantDirectoryRep.AddRangeAsync(tenantDirectorieList);
            await dbContext.TenantMenuRep.AddRangeAsync(tenantMenuList);
            await dbContext.TenantMenuButtonRep.AddRangeAsync(tenantMenuButtonList);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 新增租户菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> PushTenantMenu(long menuId, long directoryId, long tenantId)
        {
            T_Menu menu = await dbContext.Set<T_Menu>().Where(p => p.Id == menuId && p.Weight == (int)MenuWeightTypeEnum.Service)
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
            T_Tenant tenant = await dbContext.TenantRep.FirstOrDefaultAsync(p => p.Id == id);
            tenant.InviteCode = inviteCode;
            dbContext.TenantRep.Update(tenant);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 修改租户信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateTenant(string name, string code, long Id)
        {
            T_Tenant tenant = await dbContext.TenantRep.FirstOrDefaultAsync(p => p.Id == Id);
            tenant.Name = name;
            tenant.Code = code;
            dbContext.TenantRep.Update(tenant);
            await dbContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
