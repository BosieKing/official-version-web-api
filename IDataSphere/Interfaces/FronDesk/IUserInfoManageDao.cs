using Model.Repositotys.Service;

namespace IDataSphere.Interfaces.FronDesk
{
    /// <summary>
    /// 前台用户中心数据访问接口
    /// </summary>
    /// <remarks>T_User</remarks>
    public interface IUserInfoManageDao : IBaseDao<T_User>
    {
        Task<dynamic> GetUserInfoById(long userId);
        Task<bool> UpdateAvatar(string url, long userId);
        Task<bool> UpdatePassword(string newPassword, long userId);
        Task<bool> UpdateUserInfo(T_User user);
    }
}