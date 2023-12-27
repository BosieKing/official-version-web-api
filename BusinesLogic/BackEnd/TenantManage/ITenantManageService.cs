using BusinesLogic.BackEnd.TenantManage.Dto;
using IDataSphere.Interface.BackEnd.TenantManage;
using SharedLibrary.Models.DomainModels;
using SharedLibrary.Models.SharedDataModels;

namespace BusinesLogic.BackEnd.TenantManage
{
    /// <summary>
    /// 租户管理接口
    /// </summary>
    public interface ITenantManageService
    {
        Task<bool> AddTenant(AddTenantInput input);
        Task<bool> UpdateTenant(UpdateTenantInput input);
        Task<bool> UptateInviteCode(long tenantId);
        Task<PaginationResultModel> GetTenantPage(GetTenantPageInput input);
        Task<List<DropdownSelectionModel>> GetTenantMenuList(IdInput input);
        Task<List<DropdownDataModel>> GetTenantDirectoryList(long tenantId);
        Task<bool> PushTenantMenu(PushTenantMenuInput input);
    }
}
