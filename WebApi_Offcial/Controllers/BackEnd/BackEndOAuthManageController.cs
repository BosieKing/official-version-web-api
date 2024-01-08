using BusinesLogic.BackEnd.BackEndOAuthManage;
using BusinesLogic.Center.Captcha;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.CoreData;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.BackEndOAuthManage;
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
    [ServiceFilter(typeof(BackEndOAuthManageActionFilter))]
    public class BackEndOAuthManageController : ControllerBase
    {
        #region 构造函数
        private readonly ICaptchaService _captchaService;
        private readonly IBackEndOAuthManageService _backEndOAuthManageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 构造函数
        /// </summary>
        public BackEndOAuthManageController(IBackEndOAuthManageService backEndOAuthManageService,
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
        public async Task<ActionResult<DataResponseModel>> getMenuTree()
        {
            bool isSuperManage = bool.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.IS_SUPERMANAGE)?.Value ?? "false");
            List<MenuTreeModel> list;
            if (isSuperManage)
            {
                list = await _backEndOAuthManageService.GetSuperManageMenuTree();
            }
            else
            {
                string roleIds = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.ROLE_ID).Value;
                list = await _backEndOAuthManageService.GetMenuTree(roleIds);
            }
            return DataResponseModel.SetData(list);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUserInfo")]
        public async Task<ActionResult<DataResponseModel>> GetUserInfo()
        {
            // todo:获取用户信息，完成
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value;
            var data = await _backEndOAuthManageService.GetUserInfo(long.Parse(userId));
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 返回拥有的按钮信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("getButtonArray")]
        public async Task<ActionResult<DataResponseModel>> GetButtonArray()
        {
            // todo:返回拥有的按钮信息 ，完成
            bool isSuperManage = bool.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.IS_SUPERMANAGE)?.Value ?? "false");
            if (isSuperManage)
            {
                string[] list = await _backEndOAuthManageService.GetSuperManageButtonArray();
                return DataResponseModel.SetData(list);
            }
            else
            {
                var roleIds = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.ROLE_ID).Value;
                var list = await _backEndOAuthManageService.GetButtonArray(roleIds);
                return DataResponseModel.SetData(list);
            }
        }

        /// <summary>
        /// 获取已绑定的平台
        /// </summary>
        /// <returns></returns>
        [HttpGet("getBindTenantList")]
        public async Task<ActionResult<DataResponseModel>> getBindTenantList()
        {
            // todo:获取已绑定的平台，待测试
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            bool isSuperManage = bool.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.IS_SUPERMANAGE)?.Value ?? "false");
            if (isSuperManage)
            {
                var data = await _backEndOAuthManageService.GetSuperManageBindTenantList(userId);
                return DataResponseModel.SetData(data);
            }
            else
            {
                var data = await _backEndOAuthManageService.GetBindTenantList(userId);
                return DataResponseModel.SetData(data);
            }

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
        public async Task<ActionResult<DataResponseModel>> LoginByPassword([FromBody] BackEndLoginByPasswordInput input)
        {
            // 设置返回头
            var result = await _backEndOAuthManageService.LoginByPassWord(input.IsRemember, phone: input.Phone);
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
        public async Task<ActionResult<DataResponseModel>> LoginByVerifyCode([FromBody] BackEndLoginByVerifyCodeInput input)
        {
            // 设置返回头
            var result = await _backEndOAuthManageService.LoginByPassWord(input.IsRemember, phone: input.Phone);
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
        public async Task<ActionResult<DataResponseModel>> SwitchTenant([FromBody] IdInput input)
        {
            // todo:切换平台，重新获取租户token，待测试
            // 设置返回头
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            long tenantId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.TENANT_ID).Value);
            bool isRemember = bool.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.IS_REMEMBER).Value);
            var result = await _backEndOAuthManageService.LoginByPassWord(isRemember, userId: userId, tenantId: tenantId);
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
            var result = await RedisMulititionHelper.LoginOut(userId.ToString(), token);
            return DataResponseModel.SetData(result);
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updateUserInfo")]
        public async Task<ActionResult<DataResponseModel>> UpdateUserInfo([FromBody] UpdateUserInfoInput input)
        {
            // todo:修改用户信息
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            var result = await _backEndOAuthManageService.UpdateUserInfo(input, userId);
            return DataResponseModel.SetData(result);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("updatePassword")]
        public async Task<ActionResult<DataResponseModel>> UpdatePassword(BackEndUpdatePasswordInput input)
        {
            // todo:修改密码
            // 设置返回头
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            var result = await _backEndOAuthManageService.UpdatePassword(input, userId);
            return DataResponseModel.SetData(result);
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
            bool data = await _backEndOAuthManageService.UploadAvatar(url, userId);
            return DataResponseModel.SetData(data);
        }
        #endregion

        #region 辅助
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
        /// 发送登录验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost("sendLoginVerifyCode")]
        [AllowAnonymous]
        public async Task<ActionResult<DataResponseModel>> SendLoginVerifyCode([FromBody]
        [Required(ErrorMessage = "PhoneRequired")]
        [RegularExpression(@"^1[3|4|5|7|8|9][0-9]{9}$", ErrorMessage = "PhoneFormatError")] string phone)
        {
            var data = await _captchaService.SendPhoneCode(VerificationCodeTypeEnum.Login, phone: phone);
            return DataResponseModel.SetData(data);
        }

        /// <summary>
        /// 发送修改密码验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost("sendUpdatePwdVerifyCode")]
        [AllowAnonymous]
        public async Task<ActionResult<DataResponseModel>> SendUpdatePwdVerifyCode()
        {
            // todo:发送修改密码验证码
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            var data = await _captchaService.SendPhoneCode(VerificationCodeTypeEnum.UpdatePwd, userId: userId);
            return DataResponseModel.SetData(data);
        }
        #endregion
    }
}
