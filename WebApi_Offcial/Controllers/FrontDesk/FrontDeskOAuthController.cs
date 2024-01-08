using BusinesLogic.Center.Captcha;
using BusinesLogic.FrontDesk.FrontDeskOAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.DTOs.Center.Captch;
using Model.DTOs.FronDesk.FrontDeskOAuth;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using WebApi_Offcial.ActionFilters.FrontDesk;

namespace WebApi_Offcial.Controllers.FrontDesk
{
    /// <summary>
    /// 前台权限管理控制器
    /// </summary>
    [ApiController]
    [Route("FrontDeskOAuth")]
    [ApiDescription(SwaggerGroupEnum.FrontDesk)]
    [ServiceFilter(typeof(FrontDeskOAuthActionFilter))]
    public class FrontDeskOAuthController : ControllerBase
    {
        #region 参数和构造函数
        private readonly ICaptchaService _captchaService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFrontDeskOAuthService _frontDeskOAuthService;
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrontDeskOAuthController(ICaptchaService captchaService, IFrontDeskOAuthService frontDeskOAuthService, IHttpContextAccessor httpContextAccessor)
        {
            _captchaService = captchaService;
            _httpContextAccessor = httpContextAccessor;
            _frontDeskOAuthService = frontDeskOAuthService;
        }
        #endregion

        #region 注册
        /// <summary>
        /// 注册后自动登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("registered")]
        [AllowAnonymous]
        public async Task<ActionResult<DataResponseModel>> Registered([FromBody] RegisteredInput input)
        {
            var result = await _frontDeskOAuthService.Register(input);
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_Token_Head] = result.Token;
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head] = result.RefreshToken;
            return DataResponseModel.SetData(true);
        }
        #endregion

        #region 登录
        /// <summary>
        /// 密码登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("loginByPassWord")]
        [AllowAnonymous]
        public async Task<ActionResult<DataResponseModel>> LoginByPassWord([FromBody] LoginByPassWordInput input)
        {
            // 设置返回头
            var result = await _frontDeskOAuthService.LoginByPassWord(input.Phone, input.IsRemember);
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_Token_Head] = result.Token;
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head] = result.RefreshToken;
            return DataResponseModel.SetData(true);
        }

        /// <summary>
        /// 验证码登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("loginByVerifyCode")]
        [AllowAnonymous]
        public async Task<ActionResult<DataResponseModel>> LoginByVerifyCode([FromBody] LoginByVerifyCodeInput input)
        {
            // 设置返回头
            var result = await _frontDeskOAuthService.LoginByPassWord(input.Phone, input.IsRemember);
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_Token_Head] = result.Token;
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head] = result.RefreshToken;
            return DataResponseModel.SetData(true);
        }

        /// <summary>
        /// 切换平台，重新获取租户token
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("switchTenant")]
        public async Task<ActionResult<DataResponseModel>> SwitchTenant([FromBody] LoginByVerifyCodeInput input)
        {
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            string token = _httpContextAccessor.HttpContext.Request.Headers[ClaimsUserConst.HTTP_Token_Head];
            await _frontDeskOAuthService.LoginOut(userId, token);
            // 设置返回头
            var result = await _frontDeskOAuthService.LoginByPassWord(input.Phone, input.IsRemember);
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_Token_Head] = result.Token;
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head] = result.RefreshToken;
            return DataResponseModel.SetData(true);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpPost("loginOut")]
        [AllowAnonymous]
        public async Task<ActionResult<DataResponseModel>> LoginOut()
        {
            string token = _httpContextAccessor.HttpContext.Request.Headers[ClaimsUserConst.HTTP_Token_Head];
            string refreshToken = _httpContextAccessor.HttpContext.Request.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head];
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            var result = await _frontDeskOAuthService.LoginOut(userId, token);
            return DataResponseModel.SetData(result);
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("forgotPassword")]
        [AllowAnonymous]
        public async Task<ActionResult<DataResponseModel>> ForgotPassword([FromBody] ForgotPasswordInput input)
        {
            bool result = await _frontDeskOAuthService.ForgotPassword(input);
            return DataResponseModel.SetData(result);
        }
        #endregion

        #region 验证码服务
        /// <summary>
        /// 获取滑动验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("getGraphicCaptcha")]
        [AllowAnonymous]
        public async Task<ActionResult<DataResponseModel>> GetGraphicCaptcha()
        {
            dynamic data = await _captchaService.GetGraphicCaptcha();
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 发送注册验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("sendRegisterVerifyCode")]
        [AllowAnonymous]
        public async Task<ActionResult<DataResponseModel>> SendRegisterVerifyCode([FromBody] SendPhoneCodeInput input)
        {
            var data = await _captchaService.SendPhoneCode(VerificationCodeTypeEnum.Register, phone: input.Phone);
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 发送忘记密码验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("sendForgetPwdVerifyCode")]
        [AllowAnonymous]
        public async Task<ActionResult<DataResponseModel>> SendForgetPwdVerifyCode([FromBody] SendPhoneCodeInput input)
        {
            var data = await _captchaService.SendPhoneCode(VerificationCodeTypeEnum.ForgetPwd, phone: input.Phone);
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 发送登录验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("sendLoginVerifyCode")]
        [AllowAnonymous]
        public async Task<ActionResult<DataResponseModel>> SendLoginVerifyCode([FromBody] SendPhoneCodeInput input)
        {
            var data = await _captchaService.SendPhoneCode(VerificationCodeTypeEnum.Login, phone: input.Phone);
            return DataResponseModel.SetData(data);
        }
        #endregion

    }
}
