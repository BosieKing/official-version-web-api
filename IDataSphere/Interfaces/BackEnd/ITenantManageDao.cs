using Model.Commons.Domain;
using Model.DTOs.BackEnd.TenantManage;
using Model.Repositotys.Service;

namespace IDataSphere.Interfaces.BackEnd
{
    /// <summary>
    /// 后台租户管理数据访问接口
    /// </summary>
    /// <remarks>T_Tenant</remarks>
    public interface ITenantManageDao : IBaseDao<T_Tenant>
    {
        Task<PageResult> GetTenantPage(GetTenantPageInput input);
        Task<bool> UpdateTenant(string name, string code, long Id);
        Task<bool> UptateInviteCode(long id, string inviteCode);
    }
}
