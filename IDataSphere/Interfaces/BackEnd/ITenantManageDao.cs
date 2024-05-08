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
        Task<List<(long Id, string Router)>> AddTenantMenu(long tenantId, long userId);
        Task<PageResult> GetTenantPage(GetTenantPageInput input);
        Task<bool> PushTenantMenu(long menuId, long directoryId, long tenantId);
        Task<bool> UpdateTenant(string name, string code, long Id);
        Task<bool> UptateInviteCode(long id, string inviteCode);
    }
}
