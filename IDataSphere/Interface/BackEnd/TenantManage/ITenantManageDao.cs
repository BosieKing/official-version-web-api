using IDataSphere.Repositoty;
using SharedLibrary.Models.DomainModels;

namespace IDataSphere.Interface.BackEnd.TenantManage
{
    /// <summary>
    /// 用户管理访问数据库接口
    /// </summary>
    public interface ITenantManageDao : IBaseDao<T_Tenant>
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
