using DataSphere.ES;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.DTOs.Center.Captch;
using Model.DTOs.FronDesk.FrontDeskOAuth;
using Model.DTOs.FronDesk.PostHomePage;
using Service.Center.Captcha;
using Service.FrontDesk.FrontDeskOAuth;
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
        public FrontDeskOAuthController(
            ICaptchaService captchaService,
            IFrontDeskOAuthService frontDeskOAuthService,
            IHttpContextAccessor httpContextAccessor)
        {
            _captchaService = captchaService;
            _httpContextAccessor = httpContextAccessor;
            _frontDeskOAuthService = frontDeskOAuthService;
        }
        #endregion

        #region 注册
       
        #endregion
    }
}
