using Model.Commons.Domain;
using Model.DTOs.BackEnd.MenuManage;

namespace BusinesLogic.BackEnd.MenuManage
{
    /// <summary>
    /// 后台菜单库管理业务接口
    /// </summary>
    public interface IMenuManageService
    {
        Task<bool> AddMenu(AddMenuInput input);
        Task<bool> AddMenuButton(AddMenuButtonInput input);
        Task<bool> DeleteMenu(long id);
        Task<PageResult> GetMenuPage(GetMenuPageInput input);
        Task<bool> UpdateMenu(UpdateMeunInput input);
        Task<bool> AddDirectory(AddDirectoryInput input);
        Task<List<DropdownDataResult>> GetDirectoryList();
        Task<bool> UpdateDirectory(UpdateDirectoryInput input);
        Task<bool> UpdateMenuButton(UpdateMenuButtonInput input);
        Task<bool> DeleteMenuButton(long id);
        Task<bool> DeleteDirectory(long id);
        Task<List<DropdownDataResult>> GetMenuList();
    }
}