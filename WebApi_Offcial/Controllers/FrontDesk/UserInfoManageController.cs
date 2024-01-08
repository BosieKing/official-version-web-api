using BusinesLogic.Center.Captcha;
using BusinesLogic.FrontDesk.UserInfoManage;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.DTOs.FronDesk.UserInfoManage;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;
using WebApi_Offcial.ActionFilters.FrontDesk;

namespace WebApi_Offcial.Controllers.FrontDesk
{
    /// <summary>
    /// 用户中心管理控制器
    /// </summary>
    [Route("UserInfoManage")]
    [ApiController]
    [ApiDescription(SwaggerGroupEnum.FrontDesk)]
    [ServiceFilter(typeof(UserInfoManageActionFilter))]
    public class UserInfoManageController : ControllerBase
    {
        #region 构造函数
        private readonly IUserInfoManageService _userInfoManageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICaptchaService _captchaService;
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserInfoManageController(ICaptchaService captchaService, IHttpContextAccessor httpContextAccessor, IUserInfoManageService userInfoManageService)
        {
            _captchaService = captchaService;
            _httpContextAccessor = httpContextAccessor;
            _userInfoManageService = userInfoManageService;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUserInfo")]
        public async Task<ActionResult<DataResponseModel>> GetUserInfo()
        {
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            var data = await _userInfoManageService.GetUserInfo(userId);
            return DataResponseModel.SetData(data);
        }
        #endregion

        #region 新增
        #endregion

        #region 修改
        /// <summary>
        /// 完善资料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("completeUserInfo")]
        public async Task<ActionResult<DataResponseModel>> CompleteUserInfo([FromBody] CompleteUserInfoInput input)
        {
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            bool data = await _userInfoManageService.UpdateUserInfo(input, userId);
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [RequestFormLimits(MultipartBodyLengthLimit = 268435456)]
        [RequestSizeLimit(268435456)]
        [HttpPost("uploadAvatar")]
        public async Task<ActionResult<DataResponseModel>> UploadAvatar([FromForm] IFormFile formFile)
        {
            string url = "测试头像地址";
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            bool data = await _userInfoManageService.UploadAvatar(url, userId);
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 通过原密码修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updatePassword")]
        public async Task<ActionResult<DataResponseModel>> UpdatePassword([FromBody] UpdatePasswordInput input)
        {
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            string token = _httpContextAccessor.HttpContext.Request.Headers[ClaimsUserConst.HTTP_Token_Head];
            bool data = await _userInfoManageService.UpdatePassword(input.NewPassword, userId, token);
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 通过验证码修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updatePasswordByCode")]
        public async Task<ActionResult<DataResponseModel>> UpdatePasswordByCode([FromBody] UpdatePasswordByCodeInput input)
        {
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            string token = _httpContextAccessor.HttpContext.Request.Headers[ClaimsUserConst.HTTP_Token_Head];
            bool data = await _userInfoManageService.UpdatePassword(input.NewPassword, userId, token);
            return DataResponseModel.SetData(data);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 注销账号
        /// </summary>
        /// <returns></returns>
        [HttpPost("cancel")]
        public async Task<ActionResult<DataResponseModel>> Cancel()
        {
            return DataResponseModel.Successed();
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("loginOut")]
        public async Task<ActionResult<DataResponseModel>> LoginOut()
        {
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            string token = Request.Headers[ClaimsUserConst.HTTP_Token_Head].ToString();
            string referenceToken = Request.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head].ToString();
            var result = await RedisMulititionHelper.LoginOut(userId.ToString(), token);
            return DataResponseModel.Successed();
        }
        #endregion

        #region 辅助方法      
        /// <summary>
        /// 发送修改密码验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost("updatePwdSendPhoneCode")]
        public async Task<ActionResult<DataResponseModel>> UpdatePwdSendPhoneCode()
        {
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            var data = await _captchaService.SendPhoneCode(VerificationCodeTypeEnum.UpdatePwd, userId: userId);
            return DataResponseModel.SetData(data);
        }
        #endregion
    }
}
