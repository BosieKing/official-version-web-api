using IDataSphere.Repositoty;
using SharedLibrary.Models.DomainModels;

namespace IDataSphere.Interface.BackEnd.UserManage
{
    /// <summary>
    /// 用户管理数据访问接口
    /// </summary>
    public interface IUserManageDao : IBaseDao<T_User>
    {
        Task<PaginationResultModel> GetUserPage(GetUserPageInput input);
        Task<List<DropdownSelectionModel>> GetUserRoleList(long userId);
        Task<bool> ResetPassword(long userId, string pwd);
        Task<bool> UpdateIsDisableLogin(long userId, bool isDisableLogin);    
        Task<bool> UpdateUser(T_User newUser);
    }
}
