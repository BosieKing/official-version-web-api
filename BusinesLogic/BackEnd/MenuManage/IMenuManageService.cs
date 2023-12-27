using BusinesLogic.BackEnd.MenuManage.Dto;
using IDataSphere.Interface.BackEnd.MenuManage;
using SharedLibrary.Models.DomainModels;

namespace BusinesLogic.BackEnd.MenuManage
{
    public interface IMenuManageService
    {
        Task<bool> AddMenu(AddMenuInput input);
        Task<bool> AddMenuButton(AddMenuButtonInput input);
        Task<bool> DelteMenu(long id);
        Task<PaginationResultModel> GetMenuPage(GetMenuPageInput input);
        Task<bool> UpdateMenu(UpdateMeunInput input);
        Task<bool> AddDirectory(AddDirectoryInput input);
        Task<List<DropdownDataModel>> GetDirectoryList();
        Task<bool> UpdateDirectory(UpdateDirectoryInput input);
        Task<bool> UpdateMenuButton(UpdateMenuButtonInput input);
        Task<bool> DelteMenuButton(long id);
        Task<bool> DeleteDirectory(long id);
        Task<List<DropdownDataModel>> GetMenuList();
    }
}