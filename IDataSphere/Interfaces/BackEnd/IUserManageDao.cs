using Model.Commons.Domain;
using Model.DTOs.BackEnd.UserManage;
using Model.Repositotys.Service;

namespace IDataSphere.Interfaces.BackEnd
{
    /// <summary>
    /// 后台用户管理数据访问接口
    /// </summary>
    /// <remarks>T_User</remarks>
    public interface IUserManageDao : IBaseDao<T_User>
    {
        Task<PageResult> GetUserPage(GetUserPageInput input);
        Task<bool> ResetPassword(long userId, string pwd);
        Task<bool> UpdateIsDisableLogin(long userId, bool isDisableLogin);
        Task<bool> UpdateUser(T_User newUser);
    }
}
