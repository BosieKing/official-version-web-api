using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.RoleManage;

namespace Service.BackEnd.RoleManage
{
    /// <summary>
    /// 后台角色管理业务接口
    /// </summary>
    public interface IRoleManageService
    {
        Task<bool> AddRole(AddRoleInput input);
        Task<bool> UpdateRole(UpdateRoleInput input);
        Task<PageResult> GetRolePage(GetRolePageInput input);
        Task<bool> AddRoleMenu(AddRoleMenuInput input, string tenantId);
        Task<bool> DeleteRole(long id);
        Task<dynamic> GetRoleMenuList(IdInput input);
    }
}
