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
    }
}
