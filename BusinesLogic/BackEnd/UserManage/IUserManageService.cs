using Model.Commons.Domain;
using Model.DTOs.BackEnd.UserManage;

namespace BusinesLogic.BackEnd.UserManage
{
    /// <summary>
    /// 后台用户管理业务接口
    /// </summary>
    public interface IUserManageService
    {
        Task<bool> UpdateUser(UpdateUserInput input);
        Task<bool> UpdateIsDisableLogin(UpdateIsDisableLoginInput input);
        Task<PageResult> GetUserPage(GetUserPageInput input);
        Task<bool> AddUser(AddUserInput input);
        Task<bool> ResetPassword(ResetPasswordInput input);
        Task<List<DropdownSelectionResult>> GetUserRoleList(long id);
        Task<bool> AddUserRole(AddUserRoleInput input);
    }
}
