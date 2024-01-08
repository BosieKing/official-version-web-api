using BusinesLogic.Center.Captcha;
using IDataSphere.Interfaces.BackEnd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.BackEndOAuthManage;
using Model.Repositotys;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;
using UtilityToolkit.Tools;
using UtilityToolkit.Utils;

namespace WebApi_Offcial.ActionFilters.BackEnd
{
    /// <summary>
    /// 后台登录验证切面
    /// </summary>
    public class BackEndOAuthManageActionFilter : IAsyncActionFilter
    {
        #region 参数和构造函数

        private readonly IBackEndOAuthDao _backEndOAuthDao;
        private readonly ICaptchaService _captchaService;
        private readonly IStringLocalizer<UserTips> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 构造函数
        /// </summary>
        public BackEndOAuthManageActionFilter(IBackEndOAuthDao backEndOAuthDao,
            IStringLocalizer<UserTips> stringLocalizer, ICaptchaService captchaService,
            IHttpContextAccessor httpContextAccess)
        {
            _httpContextAccessor = httpContextAccess;
            _backEndOAuthDao = backEndOAuthDao;
            _stringLocalizer = stringLocalizer;
            _captchaService = captchaService;
        }

        #endregion

        #region 切面 

        /// <summary>
        /// 验证切面
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string actionName = context.RouteData.Values["action"].ToString().ToLower();
            Dictionary<string, object> dic = (Dictionary<string, object>)context.ActionArguments;
            DataResponseModel serviceResult = null;
            switch (actionName)
            {
                case "loginbypassword":
                    BackEndLoginByPasswordInput input = (BackEndLoginByPasswordInput)dic["input"];
                    serviceResult = await LoginByPassWordVerify(input);
                    break;
                case "loginbyverifycode":
                    serviceResult = await LoginByVerifyCodeVerify((BackEndLoginByVerifyCodeInput)dic["input"]);
                    break;
                case "switchtenant":
                    serviceResult = await SwitchTenantVerify((IdInput)dic["input"]);
                    break;
                case "updatepassword":
                    serviceResult = await UpdatePasswordVerify((BackEndUpdatePasswordInput)dic["input"]);
                    break;
                default:
                    serviceResult = DataResponseModel.Successed();
                    break;
            }
            // 不成功则返回
            if (!serviceResult.Success)
            {
                context.Result = new JsonResult(serviceResult);
                return;
            }
            await next.Invoke();
        }

        #endregion

        #region  验证方法
        /// <summary>
        /// 密码登录验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<DataResponseModel> LoginByPassWordVerify(BackEndLoginByPasswordInput input)
        {
            // 验证滑动验证码值
            var result = await _captchaService.GraphicCaptchaVerify(input.Guid, input.GraphicCaptcha);
            if (!result.Success)
            {
                return result;
            }
            // 密码出错有效期 
            int pwdExpirationTime = ConfigSettingTool.CaptchaConfigOptions.PasswordErrorCountExpirationTime * 60;
            // 密码最大出错次数
            int pwdErrorMaxCount = ConfigSettingTool.CaptchaConfigOptions.PasswordErrorMaxCount;
            // 密码出错次数Key
            string passwordErrorCountKey = CaptchaCacheConst.PASSWORD_ERROR_COUNT_KEY + input.Phone;
            var redisClient = RedisMulititionHelper.GetClinet(CacheTypeEnum.Verify);
            // 获取密码已出错次数
            string passwordErrorCountValue = redisClient.Get(passwordErrorCountKey);
            int passwordErrorCount = passwordErrorCountValue.IsNullOrEmpty() ? 0 : int.Parse(passwordErrorCountValue);
            // 判断密码出错是否已经达到最大次数了
            if (passwordErrorCount >= pwdErrorMaxCount)
            {
                // 剩余过期时间
                long expTime = redisClient.Ttl(passwordErrorCountKey);
                return DataResponseModel.IsFailure(_stringLocalizer["PasswrodMaxErrorWait"].Value.Replace("@", $"{expTime / 60}"));
            }
            // 判断密码是否正确
            bool passWordExist = await _backEndOAuthDao.PassWordInManageExiste(input.Phone, input.Password);
            // 密码不存在
            if (!passWordExist)
            {
                passwordErrorCount++;
                redisClient.Set(passwordErrorCountKey, passwordErrorCount, pwdExpirationTime);
                return DataResponseModel.IsFailure(_stringLocalizer["IsNotManage"].Value.Replace("@", $"{pwdErrorMaxCount - passwordErrorCount}"));
            }
            // 走到这里代表密码、验证码完全匹配
            // 删除Key
            redisClient.Del(passwordErrorCountKey);
            return DataResponseModel.Successed();
        }


        /// <summary>
        /// 验证码登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<DataResponseModel> LoginByVerifyCodeVerify(BackEndLoginByVerifyCodeInput input)
        {
            // 判断账号是否注册
            bool accountExist = await _backEndOAuthDao.IsManage(input.Phone);
            if (!accountExist)
            {
                return DataResponseModel.IsFailure(_stringLocalizer["IsNotManage"].Value);
            }
            // 获取缓存中的滑动验证码值
            var redisClient = RedisMulititionHelper.GetClinet(CacheTypeEnum.Verify);
            // 滑动验证码Key
            string graphicCaptchaKey = CaptchaCacheConst.GRAPHIC_CAPTCHA_KEY + input.Guid;
            // 密码出错次数Key
            string passwordErrorCountKey = CaptchaCacheConst.PASSWORD_ERROR_COUNT_KEY + input.Phone;
            string graphicCaptchaeValue = redisClient.Get(graphicCaptchaKey);
            if (graphicCaptchaeValue.IsNullOrEmpty())
            {
                return DataResponseModel.IsFailure(_stringLocalizer["GraphicCaptchaExpired"].Value);
            }
            // 无论是否相等，进入验证立马销毁
            redisClient.Del(graphicCaptchaKey);
            if (input.GraphicCaptcha != graphicCaptchaeValue)
            {
                return DataResponseModel.IsFailure(_stringLocalizer["GraphicCaptchaError"].Value);
            }
            // 验证验证码是否匹配
            var result = await _captchaService.PhoneCodeVerify(VerificationCodeTypeEnum.Login, input.Phone, input.VerifyCode);
            if (!result.Success)
            {
                return result;
            }
            return DataResponseModel.Successed();
        }

        /// <summary>
        /// 切换平台验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<DataResponseModel> SwitchTenantVerify(IdInput input)
        {
            // 判断用户是否在该平台担任管理员
            long uniqueNumber = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.UNIQUE_NUMBER).Value);
            bool isManage = await _backEndOAuthDao.InTenantIsManage(uniqueNumber, input.Id);
            if (!isManage)
            {
                return DataResponseModel.IsFailure(_stringLocalizer["IsNotManage"].Value);
            }
            return DataResponseModel.Successed();
        }


        /// <summary>
        /// 修改密码验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<DataResponseModel> UpdatePasswordVerify(BackEndUpdatePasswordInput input)
        {
            // 判断账号是否注册
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            bool accountExist = await _backEndOAuthDao.IdExisted<T_User>(userId);
            if (!accountExist)
            {
                return DataResponseModel.IsFailure(_stringLocalizer["UserNotRegister"].Value);
            }
            // 验证验证码是否匹配
            string phone = await _backEndOAuthDao.GetPhoneById(userId);
            var result = await _captchaService.PhoneCodeVerify(VerificationCodeTypeEnum.UpdatePwd, phone, input.VerifyCode);
            if (!result.Success)
            {
                return result;
            }
            return DataResponseModel.Successed();
        }
        #endregion
    }
}
