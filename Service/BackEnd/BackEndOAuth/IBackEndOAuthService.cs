using Model.Commons.CoreData;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.BackEndOAuth;

namespace Service.BackEnd.BackEndOAuth
{
    /// <summary>
    /// 后台权限管理业务接口
    /// </summary>
    public interface IBackEndOAuthService
    {
        Task<List<DropdownDataResult>> GetBindTenantList();

        Task<List<MenuTreeModel>> GetMenuTree();    

        Task<dynamic> GetUserInfo();
        Task<(string Token, string RefreshToken)> LoginByPassWord(bool isRemember, string phone = "", long userId = 0, long tenantId = 0);
        Task<bool> UpdatePassword(BackEndUpdatePasswordInput input, long userId);
        Task<bool> UpdateUserInfo(UpdateUserInfoInput input, long userId);
        Task<bool> UploadAvatar(string url, long userId);
    }
}