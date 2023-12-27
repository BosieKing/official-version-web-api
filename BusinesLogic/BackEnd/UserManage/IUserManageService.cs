using BusinesLogic.BackEnd.UserManage.Dto;
using IDataSphere.Interface.BackEnd.UserManage;
using SharedLibrary.Models.DomainModels;

namespace BusinesLogic.BackEnd.UserManage
{
    /// <summary>
    /// 用户管理业务接口
    /// </summary>
    public interface IUserManageService
    {
        Task<bool> UpdateUser(UpdateUserInput input);
        Task<bool> UpdateIsDisableLogin(UpdateIsDisableLoginInput input);
        Task<PaginationResultModel> GetUserPage(GetUserPageInput input);
        Task<bool> AddUser(AddUserInput input);
        Task<bool> ResetPassword(ResetPasswordInput input);
        Task<List<DropdownSelectionModel>> GetUserRoleList(long id);
        Task<bool> AddUserRole(AddUserRoleInput input);
    }
}
