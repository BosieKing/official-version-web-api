using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.CoreData;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.BackEndOAuth;
using Service.BackEnd.BackEndOAuth;
using Service.Center.Captcha;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using UtilityToolkit.Helpers;
using WebApi_Offcial.ActionFilters.BackEnd;

namespace WebApi_Offcial.Controllers.BackEnd
{
    /// <summary>
    /// 后台授权
    /// </summary>
    [ApiController]
    [Route("BackEndOAuth")]
    [ApiDescription(SwaggerGroupEnum.BackEnd)]
    [ServiceFilter(typeof(BackEndOAuthActionFilter))]
    public class BackEndOAuthController : ControllerBase
    {
        #region 构造函数
        private readonly ICaptchaService _captchaService;
        private readonly IBackEndOAuthService _backEndOAuthManageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 构造函数
        /// </summary>
        public BackEndOAuthController(IBackEndOAuthService backEndOAuthManageService,
            ICaptchaService captchaService,
            IHttpContextAccessor httpContextAccess)
        {
            _backEndOAuthManageService = backEndOAuthManageService;
            _captchaService = captchaService;
            _httpContextAccessor = httpContextAccess;

        }
        #endregion

        #region 查询
        /// <summary>
        /// 获取左侧菜单权限树
        /// </summary>
        /// <returns></returns>
        [HttpGet("getMenuTree")]
        public async Task<ActionResult<ServiceResult>> getMenuTree()
        {
            var result = await _backEndOAuthManageService.GetMenuTree();
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUserInfo")]
        public async Task<ActionResult<ServiceResult>> GetUserInfo()
        {
            dynamic result = await _backEndOAuthManageService.GetUserInfo();
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 获取已绑定的平台
        /// </summary>
        /// <returns></returns>
        [HttpGet("getBindTenantList")]
        public async Task<ActionResult<ServiceResult>> getBindTenantList()
        {

                var result = await _backEndOAuthManageService.GetBindTenantList();
                return ServiceResult.SetData(result);
          
        }

        /// <summary>
        /// 获取用户的一些数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("getCount")]
        public async Task<ActionResult<ServiceResult>> GetCount()
        {
            return ServiceResult.SetData(new
            {
                waitReadMessageCount = 0,
                waitAuditCount = 0,
                auditNodeConfigCount = 12
            });
        }
        #endregion

        #region 登录相关
        /// <summary>
        /// 密码登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("loginByPassword")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResult>> LoginByPassword([FromBody] BackEndLoginByPasswordInput input)
        {
            // 设置返回头
            (string Token, string RefreshToken) result = await _backEndOAuthManageService.LoginByPassWord(input.IsRemember, phone: input.Phone);
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_Token_Head] = result.Token;
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head] = result.RefreshToken;
            return ServiceResult.SetData(true);
        }

        /// <summary>
        /// 切换平台，重新获取租户token
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("switchTenant")]
        public async Task<ActionResult<ServiceResult>> SwitchTenant([FromBody] IdInput input)
        {
            // todo:切换平台，重新获取租户token，待测试
            // 设置返回头
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            long tenantId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.TENANT_ID).Value);
            bool isRemember = bool.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.IS_REMEMBER).Value);
            (string Token, string RefreshToken) result = await _backEndOAuthManageService.LoginByPassWord(isRemember, userId: userId, tenantId: tenantId);
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_Token_Head] = result.Token;
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head] = result.RefreshToken;
            return ServiceResult.SetData(true);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpPost("loginOut")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResult>> LoginOut()
        {
            string token = _httpContextAccessor.HttpContext.Request.Headers[ClaimsUserConst.HTTP_Token_Head];
            string refreshToken = _httpContextAccessor.HttpContext.Request.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head];
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            bool result = await RedisMulititionHelper.LoginOut(userId.ToString(), token);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateUserInfo")]
        public async Task<ActionResult<ServiceResult>> UpdateUserInfo([FromBody] UpdateUserInfoInput input)
        {
            // todo:修改用户信息
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            bool result = await _backEndOAuthManageService.UpdateUserInfo(input, userId);
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updatePassword")]
        public async Task<ActionResult<ServiceResult>> UpdatePassword(BackEndUpdatePasswordInput input)
        {
            // todo:修改密码
            // 设置返回头
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            bool result = await _backEndOAuthManageService.UpdatePassword(input, userId);
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
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            bool result = await _backEndOAuthManageService.UploadAvatar(url, userId);
            return ServiceResult.SetData(result);
        }
        #endregion

        #region 验证码相关
        /// <summary>
        /// 获取滑动验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("getGraphicCaptcha")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResult>> GetGraphicCaptcha()
        {
            dynamic result = await _captchaService.GetGraphicCaptcha();
            return ServiceResult.SetData(result);
        }

        /// <summary>
        /// 发送登录验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost("sendLoginVerifyCode")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResult>> SendLoginVerifyCode([FromBody]
        [Required(ErrorMessage = "PhoneRequired")]
        [RegularExpression(@"^1[3|4|5|7|8|9][0-9]{9}$", ErrorMessage = "PhoneFormatError")] string phone)
        {
            return await _captchaService.SendPhoneCode(VerificationCodeTypeEnum.Login, phone: phone);
        }

        /// <summary>
        /// 发送修改密码验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost("sendUpdatePwdVerifyCode")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResult>> SendUpdatePwdVerifyCode()
        {
            // todo:发送修改密码验证码
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            return await _captchaService.SendPhoneCode(VerificationCodeTypeEnum.UpdatePwd, userId: userId);
        }
        #endregion
    }
}
