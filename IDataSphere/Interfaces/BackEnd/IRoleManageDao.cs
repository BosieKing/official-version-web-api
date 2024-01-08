using Model.Commons.Domain;
using Model.DTOs.BackEnd.RoleManage;

namespace IDataSphere.Interfaces.BackEnd
{
    /// <summary>
    /// 后台角色管理数据访问接口
    /// </summary>
    /// <remarks>T_Role表</remarks>
    public interface IRoleManageDao : IBaseDao
    {
        Task<dynamic> GetRoleMenuList(long roleId);
        Task<PaginationResultModel> GetRolePage(GetRolePageInput input);
    }
}
