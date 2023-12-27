using IDataSphere.Repositoty;
using SharedLibrary.Models.DomainModels;

namespace IDataSphere.Interface.FronDesk
{
    /// <summary>
    /// 用户中心接口
    /// </summary>
    public interface IUserInfoManageDao : IBaseDao<T_User>
    {
        Task<T_User> GetUserInfoById(long userId);   
        Task<bool> UpdateAvatar(string url, long userId);
        Task<bool> UpdatePassword(string newPassword, long userId);
        Task<bool> UpdateUserInfo(T_User user);       
        Task<List<DropdownDataModel>> GetTenantBindList(long userId);      
        Task<(string Password, string Phone)> GetUserPassword(long userId);
    }
}