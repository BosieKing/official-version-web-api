using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.TenantManage;

namespace Service.BackEnd.TenantManage
{
    /// <summary>
    /// 后台租户管理业务接口
    /// </summary>
    public interface ITenantManageService
    {
        Task<bool> AddTenant(AddTenantInput input);
        Task<bool> UpdateTenant(UpdateTenantInput input);
        Task<bool> UptateInviteCode(long tenantId);
        Task<PageResult> GetTenantPage(GetTenantPageInput input);
        Task<List<DropdownSelectionResult>> GetTenantMenuList(IdInput input);
        Task<List<DropdownDataResult>> GetTenantDirectoryList(long tenantId);
        Task<bool> PushTenantMenu(PushTenantMenuInput input);
    }
}
