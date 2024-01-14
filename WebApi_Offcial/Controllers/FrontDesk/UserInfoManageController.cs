using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.DTOs.FronDesk.UserInfoManage;
using Service.Center.Captcha;
using Service.FrontDesk.UserInfoManage;
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
        public UserInfoManageController(
            ICaptchaService captchaService,
            IHttpContextAccessor httpContextAccessor,
            IUserInfoManageService userInfoManageService)
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
        public async Task<ActionResult<ServiceResult>> GetUserInfo()
        {           
            dynamic result = await _userInfoManageService.GetUserInfo();
            return ServiceResult.SetData(result);
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
        public async Task<ActionResult<ServiceResult>> CompleteUserInfo([FromBody] CompleteUserInfoInput input)
        {
            bool result = await _userInfoManageService.UpdateUserInfo(input);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [RequestFormLimits(MultipartBodyLengthLimit = 268435456)]
        [RequestSizeLimit(268435456)]
        [HttpPost("uploadAvatar")]
        public async Task<ActionResult<ServiceResult>> UploadAvatar([FromForm] IFormFile formFile)
        {
            string url = "测试头像地址";
            bool result = await _userInfoManageService.UploadAvatar(url);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 通过原密码修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updatePassword")]
        public async Task<ActionResult<ServiceResult>> UpdatePassword([FromBody] UpdatePasswordInput input)
        {           
            string token = _httpContextAccessor.HttpContext.Request.Headers[ClaimsUserConst.HTTP_Token_Head];
            bool result = await _userInfoManageService.UpdatePassword(input.NewPassword, token);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 通过验证码修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updatePasswordByCode")]
        public async Task<ActionResult<ServiceResult>> UpdatePasswordByCode([FromBody] UpdatePasswordByCodeInput input)
        {          
            string token = _httpContextAccessor.HttpContext.Request.Headers[ClaimsUserConst.HTTP_Token_Head];
            bool result = await _userInfoManageService.UpdatePassword(input.NewPassword, token);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("loginOut")]
        public async Task<ActionResult<ServiceResult>> LoginOut()
        {
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            string token = Request.Headers[ClaimsUserConst.HTTP_Token_Head].ToString();
            string referenceToken = Request.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head].ToString();
            bool result = await RedisMulititionHelper.LoginOut(userId.ToString(), token);
            return ServiceResult.Successed();
        }
        #endregion

        #region 辅助方法      
        /// <summary>
        /// 发送修改密码验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost("updatePwdSendPhoneCode")]
        public async Task<ActionResult<ServiceResult>> UpdatePwdSendPhoneCode()
        {
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            var result = await _captchaService.SendPhoneCode(VerificationCodeTypeEnum.UpdatePwd, userId: userId);
            return ServiceResult.SetData(result);
        }
        #endregion
    }
}
