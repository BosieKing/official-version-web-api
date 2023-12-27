using IDataSphere.Repositoty;
using SharedLibrary.Models.DomainModels;

namespace IDataSphere.Interface.BackEnd.RoleManage
{
    public interface IRoleManageDao : IBaseDao<T_Role>
    {
        Task<dynamic> GetRoleMenuList(long roleId);
        Task<PaginationResultModel> GetRolePage(GetRolePageInput input);
    }
}
