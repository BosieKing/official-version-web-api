using BusinesLogic.BackEnd.UserManage;
using BusinesLogic.BackEnd.UserManage.Dto;
using IDataSphere.Interface.BackEnd.UserManage;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Enums;
using SharedLibrary.Models.DomainModels;
using SharedLibrary.Models.SharedDataModels;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserManageController(IUserManageService userManageService, IHttpContextAccessor httpContextAccessor)
        {
            _userManageService = userManageService;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getUserPage")]
        public async Task<ActionResult<DataResponseModel>> GetUserPage([FromQuery] GetUserPageInput input)
        {
            var data = await _userManageService.GetUserPage(input);
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 查询用户已绑定的角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getUserRoleList")]
        public async Task<ActionResult<DataResponseModel>> GetUserRoleList([FromQuery] IdInput input)
        {
            var data = await _userManageService.GetUserRoleList(input.Id);
            return DataResponseModel.SetData(data);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addUser")]
        public async Task<ActionResult<DataResponseModel>> AddUser([FromBody] AddUserInput input)
        {
            var data = await _userManageService.AddUser(input);
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 绑定角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("addUserRole")]
        public async Task<ActionResult<DataResponseModel>> AddUserRole(AddUserRoleInput input)
        {
            var data = await _userManageService.AddUserRole(input);
            return DataResponseModel.SetData(data);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateUser")]
        public async Task<ActionResult<DataResponseModel>> UpdateUser([FromBody] UpdateUserInput input)
        {
            var data = await _userManageService.UpdateUser(input);
            return DataResponseModel.Successed();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("resetPassword")]
        public async Task<ActionResult<DataResponseModel>> ResetPassword([FromBody] ResetPasswordInput input)
        {
            var result = await _userManageService.ResetPassword(input);
            return DataResponseModel.Successed();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 修改是否允许登录状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateIsDisableLogin")]
        public async Task<ActionResult<DataResponseModel>> UpdateIsDisableLogin([FromBody] UpdateIsDisableLoginInput input)
        {
            var result = await _userManageService.UpdateIsDisableLogin(input);
            return DataResponseModel.SetData(result);
        }

        /// <summary>
        /// todo:移除课题组
        /// </summary>
        /// <returns></returns>
        [HttpPost("RemoveGroup")]
        public async Task<ActionResult<DataResponseModel>> RemoveGroup()
        {
            return DataResponseModel.Successed();
        }
        #endregion
    }
}
