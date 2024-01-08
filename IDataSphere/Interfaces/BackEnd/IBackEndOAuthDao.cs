using Model.Commons.CoreData;
using Model.Commons.Domain;

namespace IDataSphere.Interfaces.BackEnd
{
    /// <summary>
    /// 后台权限管理数据访问接口
    /// </summary>
    /// <remarks>T_User表</remarks>
    public interface IBackEndOAuthDao : IBaseDao
    {
        Task<string[]> GetButtonArray(long[] roleIds);
        Task<List<MenuTreeModel>> GetMenuTree(long[] rolds);
        Task<string[]> GetSuperManageButtonArray();
        Task<List<MenuTreeModel>> GetSuperManageMenuTree();
        Task<dynamic> GetUserInfoById(long id);
        Task<TokenInfoModel> GetUserInfoByPhone(string phone = "", long userId = 0, long tenantId = 0);
        Task<bool> IsManage(string phone);
        Task<bool> PassWordInManageExiste(string phone, string password);
        Task<List<DropdownDataModel>> GetBindTenantList(long userId);
        Task<TokenInfoModel> GetSuperManageUserInfoByPhone(string phone = "", long userId = 0, long tenantId = 0);
        Task<List<DropdownDataModel>> GetSuperManageBindTenantList(long userId);
        Task<bool> IsSuperManage(string phone = "", long userId = 0);
        Task<bool> InTenantIsManage(long uniqueNumber, long tenantId);
        Task<bool> UpdateAvatar(string url, long userId);
        Task<bool> UpdateUserInfo(string realName, string email, long userId);
        Task<bool> UpdatePassword(string newPassword, long userId);
        Task<string> GetPhoneById(long id);
    }
}
