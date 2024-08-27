using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.UserManage;
using Service.BackEnd.UserManage;
using SharedLibrary.Enums;
using WebApi_Offcial.ActionFilters.BackEnd;

namespace WebApi_Offcial.Controllers.BackEnd
{
    /// <summary>
    /// 用户管理控制层
    /// </summary>
    [ApiController]
    [Route("UserManage")]
    [ApiDescription(SwaggerGroupEnum.BackEnd)]
    [ServiceFilter(typeof(UserManageActionFilter))]
    public class UserManageController : BaseController
    {
        #region 构造函数
        private readonly IUserManageService _userManageService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserManageController(IUserManageService userManageService)
        {
            _userManageService = userManageService;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getUserPage")]
        public async Task<ActionResult<ServiceResult>> GetUserPage([FromQuery] GetUserPageInput input)
        {
            PageResult result = await _userManageService.GetUserPage(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 查询用户已绑定的角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getUserRoleList")]
        public async Task<ActionResult<ServiceResult>> GetUserRoleList([FromQuery] IdInput input)
        {
            List<DropdownSelectionResult> result = await _userManageService.GetUserRoleList(input.Id);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 查询用户已绑定的审核角色类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getUserAuditTypeList")]
        public async Task<ActionResult<ServiceResult>> GetUserAuditTypeList([FromQuery] IdInput input)
        {
            List<DropdownSelectionResult> result = await _userManageService.GetUserAuditTypeList(input.Id);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addUser")]
        public async Task<ActionResult<ServiceResult>> AddUser([FromBody] AddUserInput input)
        {
            bool result = await _userManageService.AddUser(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 绑定角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addUserRole")]
        public async Task<ActionResult<ServiceResult>> AddUserRole(AddUserRoleInput input)
        {
            var result = await _userManageService.AddUserRole(input);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateUser")]
        public async Task<ActionResult<ServiceResult>> UpdateUser([FromBody] UpdateUserInput input)
        {
            bool result = await _userManageService.UpdateUser(input);
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("resetPassword")]
        public async Task<ActionResult<ServiceResult>> ResetPassword([FromBody] ResetPasswordInput input)
        {
            var result = await _userManageService.ResetPassword(input);
            return ServiceResult.Successed();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 修改是否允许登录状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateIsDisableLogin")]
        public async Task<ActionResult<ServiceResult>> UpdateIsDisableLogin([FromBody] UpdateIsDisableLoginInput input)
        {
            bool result = await _userManageService.UpdateIsDisableLogin(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// todo:移除课题组
        /// </summary>
        /// <returns></returns>
        [HttpPost("RemoveGroup")]
        public async Task<ActionResult<ServiceResult>> RemoveGroup()
        {
            return ServiceResult.Successed();
        }
        #endregion
    }
}
