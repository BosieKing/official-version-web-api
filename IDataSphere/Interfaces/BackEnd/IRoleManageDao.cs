using Model.Commons.Domain;
using Model.DTOs.BackEnd.RoleManage;
using Model.Repositotys.BasicData;

namespace IDataSphere.Interfaces.BackEnd
{
    /// <summary>
    /// 后台角色管理数据访问接口
    /// </summary>
    /// <remarks>T_Role表</remarks>
    public interface IRoleManageDao : IBaseDao<T_Role>
    {
        Task<dynamic> GetRoleMenuList(long roleId);
        Task<PageResult> GetRolePage(GetRolePageInput input);
    }
}
