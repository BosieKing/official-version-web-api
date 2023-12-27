using BusinesLogic.BackEnd.TenantMenuManage.Dto;
using IDataSphere.Interface.BackEnd.TenantMenuManage;
using SharedLibrary.Models.DomainModels;

namespace BusinesLogic.BackEnd.TenantMenuManage
{
    /// <summary>
    /// 租户菜单管理业务接口
    /// </summary>
    public interface ITenantMenuManageService
    {
        Task<PaginationResultModel> GetPage(GetTenantMenuPageInput input);
        Task<List<DropdownDataModel>> GetTenantDirectory();
        Task<bool> AddTenantDirectory(AddTenantDirectoryInput input);
        Task<bool> AddTenantMenu(AddTenantMenuInput input);
        Task<bool> AddTenantMenuButton(AddTenantMenuButtonInput input);
        Task<bool> UpdateTenantDirectory(UpdateTenantDirectoryInput input);
        Task<bool> UpdateTenantMenu(UpdateTenantMenuInput input);
        Task<bool> UpdateTenantMenuButton(UpdateTenantMenuButtonInput input);
        Task<bool> DeleteTenantDirectory(long id);
        Task<bool> DeleteTenantMenu(long id);
        Task<bool> DelteTenantMenuButton(long id);
    }
}
