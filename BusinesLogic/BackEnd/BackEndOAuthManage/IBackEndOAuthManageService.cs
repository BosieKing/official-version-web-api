using BusinesLogic.BackEnd.BackEndOAuthManage.Dto;
using SharedLibrary.Models.CoreDataModels;
using SharedLibrary.Models.DomainModels;

namespace BusinesLogic.BackEnd.BackEndOAuthManage
{
    public interface IBackEndOAuthManageService
    {
        Task<List<DropdownDataModel>> GetBindTenantList(long userId);
        Task<string[]> GetButtonArray(string rolds);
        Task<List<MenuTreeModel>> GetMenuTree(string rolds);
        Task<List<DropdownDataModel>> GetSuperManageBindTenantList(long userId);
        Task<string[]> GetSuperManageButtonArray();
        Task<List<MenuTreeModel>> GetSuperManageMenuTree();
        Task<dynamic> GetUserInfo(long userId);
        Task<(string Token, string RefreshToken)> LoginByPassWord(bool isRemember, string phone = "", long userId = 0, long tenantId = 0);
        Task<bool> UpdatePassword(BackEndUpdatePasswordInput input, long userId);
        Task<bool> UpdateUserInfo(UpdateUserInfoInput input, long userId);
        Task<bool> UploadAvatar(string url, long userId);
    }
}