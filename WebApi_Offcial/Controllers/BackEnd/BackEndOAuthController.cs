using IDataSphere.Interfaces.BackEnd;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.BackEndOAuth;
using Service.Center.Captcha;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;

namespace WebApi_Offcial.Controllers.BackEnd
{
    /// <summary>
    /// 后台权限中心
    /// </summary>
    [ApiController]
    [Route("BackOAuth")]
    [ApiDescription(SwaggerGroupEnum.BackEnd)]
    public class BackEndOAuthController : BaseController
    {
        #region 构造函数
        private readonly ICaptchaService _captchaService;
        private readonly IBackOAuthDao _backOAuthDao;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 构造函数
        /// </summary>
        public BackEndOAuthController(IBackOAuthDao backOAuthDao,
            ICaptchaService captchaService,
            IHttpContextAccessor httpContextAccess)
        {
            _backOAuthDao = backOAuthDao;
            _captchaService = captchaService;
            _httpContextAccessor = httpContextAccess;

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
            (string Token, string RefreshToken) result = await _backOAuthDao.LoginByPassWord(input);
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_Token_Head] = result.Token;
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head] = result.RefreshToken;
            return ServiceResult.SetData(true);
        }
        #endregion

        #region 
        #endregion


        #region 
        #endregion


        #region 
        #endregion
    }
}
