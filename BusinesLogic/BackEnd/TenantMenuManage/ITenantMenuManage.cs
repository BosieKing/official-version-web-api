using Model.Commons.Domain;
using Model.DTOs.BackEnd.TenantMenuManage;

namespace BusinesLogic.BackEnd.TenantMenuManage
{
    /// <summary>
    /// 后台租户菜单管理业务接口
    /// </summary>
    public interface ITenantMenuManageService
    {
        Task<PageResult> GetPage(GetTenantMenuPageInput input);
        Task<List<DropdownDataResult>> GetTenantDirectory();
        Task<bool> AddTenantDirectory(AddTenantDirectoryInput input);
        Task<bool> AddTenantMenu(AddTenantMenuInput input);
        Task<bool> AddTenantMenuButton(AddTenantMenuButtonInput input);
        Task<bool> UpdateTenantDirectory(UpdateTenantDirectoryInput input);
        Task<bool> UpdateTenantMenu(UpdateTenantMenuInput input);
        Task<bool> UpdateTenantMenuButton(UpdateTenantMenuButtonInput input);
        Task<bool> DeleteTenantDirectory(long id);
        Task<bool> DeleteTenantMenu(long id);
        Task<bool> DeleteTenantMenuButton(long id);
    }
}
