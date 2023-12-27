using BusinesLogic.BackEnd.RoleManage.Dto;
using IDataSphere.Interface.BackEnd.RoleManage;
using SharedLibrary.Models.DomainModels;
using SharedLibrary.Models.SharedDataModels;

namespace BusinesLogic.BackEnd.RoleManage
{
    public interface IRoleManageService
    {
        Task<bool> AddRole(AddRoleInput input);
        Task<bool> UpdateRole(UpdateRoleInput input);
        Task<PaginationResultModel> GetRolePage(GetRolePageInput input);
        Task<bool> AddRoleMenu(AddRoleMenuInput input, string tenantId);
        Task<bool> DeleteRole(long id);
        Task<dynamic> GetRoleMenuList(IdInput input);
    }
}
