using IDataSphere.Extensions;
using IDataSphere.Interface.BackEnd.RoleManage;
using IDataSphere.Repositoty;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using SharedLibrary.Models.DomainModels;
using UtilityToolkit.Utils;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 角色管理数据层
    /// </summary>
    public class RoleManageDao : BaseDao<T_Role>, IRoleManageDao
    {
        #region 构造函数
        public RoleManageDao(SqlDbContext dMDbContext) : base(dMDbContext)
        {
        }
        #endregion

        #region 查询
        /// <summary>
        /// 获取角色配置的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<dynamic> GetRoleMenuList(long roleId)
        {
            var checkMenuList = dMDbContext.RoleMenuRep.Where(p => p.RoleId == roleId).Select(p => p.MenuId);
            var checkButtonList = dMDbContext.RoleBlockButtonsRep.Where(p => p.RoleId == roleId).Select(p => p.ButtonId);
            var buttonList = await dMDbContext.TenantMenuButtonRep.Select(p => new
            {
                p.Id,
                p.MenuId,
                p.Name,
                IsCheck = checkButtonList.Contains(p.Id),
            }).ToListAsync();
            var menuList = await dMDbContext.TenantMenuRep.Select(p => new DropdownSelectionModel
            {
                Id = p.Id,
                Name = p.Name,
                IsCheck = checkMenuList.Contains(p.Id)
            })
            .ToListAsync();
            return menuList.Select(p => new
            {
                p.Id,
                p.Name,
                p.IsCheck,
                ButtonList = buttonList.Where(b => p.Id == b.MenuId).Select(b => new DropdownSelectionModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    IsCheck = b.IsCheck,
                }).ToList()
            });
        }



        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PaginationResultModel> GetRolePage(GetRolePageInput input)
        {
            var query = from data in dMDbContext.RoleRep
                        .Where(!input.Name.IsNullOrEmpty(), p => EF.Functions.Like(p.Name, $"%{input.Name}%"))
                        .Where(!input.Remark.IsNullOrEmpty(), p => EF.Functions.Like(p.Remark, $"%{input.Remark}%"))
                        join createUser in dMDbContext.UserRep on data.CreatedUserId equals createUser.Id into createUserResult
                        from createUser in createUserResult.DefaultIfEmpty()
                        join updateUser in dMDbContext.UserRep on data.UpdateUserId equals updateUser.Id into updateUserResult
                        from updateUser in updateUserResult.DefaultIfEmpty()
                        select new
                        {
                            data.Id,
                            data.Name,
                            data.Remark,
                            CreatedName = createUser.NickName,
                            data.CreatedTime,
                            UpdateUserName = updateUser.NickName,
                            data.UpdateTime
                        };
            return await base.AdaptPage(query, input.PageSize, input.PageNo);

        }
        #endregion

        #region 新增
        #endregion

        #region 删除
        #endregion

    }
}
