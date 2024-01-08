using Model.Commons.Domain;
using Model.DTOs.BackEnd.TenantManage;
using Model.Repositotys;

namespace IDataSphere.Interfaces.BackEnd
{
    /// <summary>
    /// 后台租户管理数据访问接口
    /// </summary>
    /// <remarks>T_Tenant</remarks>
    public interface ITenantManageDao : IBaseDao
    {
        Task<bool> AddTenant(T_Tenant tenant);
        Task<bool> AddTenantMenu(long tenantId);
        Task<bool> CodeExist(string code, long id = 0);
        Task<List<DropdownSelectionModel>> GetTenantMenuList(long tenandId);
        Task<PaginationResultModel> GetTenantPage(GetTenantPageInput input);
        Task<bool> PushTenantMenu(long menuId, long directoryId, long tenantId);
        Task<bool> UpdateTenant(string name, string code, long Id);
        Task<bool> UptateInviteCode(long id, string inviteCode);
    }
}
