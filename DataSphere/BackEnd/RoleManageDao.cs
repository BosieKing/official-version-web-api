using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.BackEnd;
using Microsoft.EntityFrameworkCore;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.RoleManage;
using UtilityToolkit.Utils;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 后台角色管理数据访问实现类
    /// </summary>
    public class RoleManageDao : BaseDao, IRoleManageDao
    {
        #region 构造函数
        public RoleManageDao(SqlDbContext dbContext) : base(dbContext)
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
            IQueryable<long> checkMenuList = dbContext.RoleMenuRep.Where(p => p.RoleId == roleId).Select(p => p.MenuId);
            IQueryable<long> checkButtonList = dbContext.RoleBlockButtonsRep.Where(p => p.RoleId == roleId).Select(p => p.ButtonId);
            var buttonList = await dbContext.TenantMenuButtonRep.Select(p => new
            {
                p.Id,
                p.MenuId,
                p.Name,
                IsCheck = checkButtonList.Contains(p.Id),
            }).ToListAsync();
            var menuList = await dbContext.TenantMenuRep.Select(p => new DropdownSelectionResult
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
                ButtonList = buttonList.Where(b => p.Id == b.MenuId).Select(b => new DropdownSelectionResult
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
        public async Task<PageResult> GetRolePage(GetRolePageInput input)
        {
            var query = from data in dbContext.RoleRep
                        .Where(!input.Name.IsNullOrEmpty(), p => EF.Functions.Like(p.Name, $"%{input.Name}%"))
                        .Where(!input.Remark.IsNullOrEmpty(), p => EF.Functions.Like(p.Remark, $"%{input.Remark}%"))
                        join createUser in dbContext.UserRep on data.CreatedUserId equals createUser.Id into createUserResult
                        from createUser in createUserResult.DefaultIfEmpty()
                        join updateUser in dbContext.UserRep on data.UpdateUserId equals updateUser.Id into updateUserResult
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
