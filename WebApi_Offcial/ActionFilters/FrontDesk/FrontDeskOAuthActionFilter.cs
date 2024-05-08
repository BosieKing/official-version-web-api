using IDataSphere.Interfaces.FronDesk;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Model.Commons.Domain;
using Model.DTOs.FronDesk.FrontDeskOAuth;
using Model.Repositotys.Service;
using Service.Center.Captcha;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;
using UtilityToolkit.Tools;
namespace WebApi_Offcial.ActionFilters.FrontDesk
{
    /// <summary>
    /// 前台权限AOP
    /// </summary>
    public class FrontDeskOAuthActionFilter : IAsyncActionFilter
    {
        #region 参数和构造函数

        private readonly IFrontDeskOAuthDao _frontDeskOAuthDao;
        private readonly ICaptchaService _captchaService;
        private readonly IStringLocalizer<UserTips> _stringLocalizer;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrontDeskOAuthActionFilter(IFrontDeskOAuthDao frontDeskOAuthDao, IStringLocalizer<UserTips> stringLocalizer, ICaptchaService captchaService)
        {
            _frontDeskOAuthDao = frontDeskOAuthDao;
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
            ServiceResult serviceResult = null;
            switch (actionName)
            {
                case "registered":
                    serviceResult = await RegisteredVerify((RegisteredInput)dic["input"]);
                    break;
                case "loginbypassword":
                    LoginByPassWordInput input = (LoginByPassWordInput)dic["input"];
                    serviceResult = await LoginByPassWordVerify(input);
                    break;
                case "loginbyverifycode":
                    serviceResult = await LoginByVerifyCodeVerify((LoginByVerifyCodeInput)dic["input"]);
                    break;
                case "forgotpassword":
                    serviceResult = await ForgotPasswordVerify((ForgotPasswordInput)dic["input"]);
                    break;
                default:
                    serviceResult = ServiceResult.Successed();
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
        private async Task<ServiceResult> LoginByPassWordVerify(LoginByPassWordInput input)
        {
            // 验证滑动验证码值
            ServiceResult captchaResult = await _captchaService.GraphicCaptchaVerify(input.Guid, input.GraphicCaptcha);
            if (!captchaResult.Success)
            {
                return captchaResult;
            }
            // 判断账号是否注册
            bool accountExist = await _frontDeskOAuthDao.DataExisted<T_User>(p => p.Phone == input.Phone, true);
            if (!accountExist)
            {
                return ServiceResult.IsFailure(_stringLocalizer["UserNotRegister"].Value);
            }
            // 允许密码出错有效时间
            int pwdExpirationTime = ConfigSettingTool.CaptchaConfigOptions.PasswordErrorCountExpirationTime * 60;
            // 允许密码最大出错次数
            int pwdErrorMaxCount = ConfigSettingTool.CaptchaConfigOptions.PasswordErrorMaxCount;
            // 密码已出错次数Key
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
                return ServiceResult.IsFailure(_stringLocalizer["PasswrodErrorWait"].Value.Replace("@", $"{expTime / 60}"));
            }
            // 判断密码是否匹配
            bool passWordExist = await _frontDeskOAuthDao.DataExisted<T_User>(p => p.Phone == input.Phone && p.Password == input.Password, true);
            // 密码不匹配
            if (!passWordExist)
            {
                passwordErrorCount++;
                redisClient.Set(passwordErrorCountKey, passwordErrorCount, pwdExpirationTime);
                return ServiceResult.IsFailure(_stringLocalizer["PasswrodError"].Value.Replace("@", $"{pwdErrorMaxCount - passwordErrorCount}"));
            }
            // 完全匹配删除Key
            redisClient.Del(passwordErrorCountKey);
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 验证码登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> LoginByVerifyCodeVerify(LoginByVerifyCodeInput input)
        {
            // 获取缓存中的滑动验证码值
            var redisClient = RedisMulititionHelper.GetClinet(CacheTypeEnum.Verify);
            // 验证滑动验证码值
            ServiceResult captchaResult = await _captchaService.GraphicCaptchaVerify(input.Guid, input.GraphicCaptcha);
            if (!captchaResult.Success)
            {
                return captchaResult;
            }
            // 判断账号是否注册
            bool accountExist = await _frontDeskOAuthDao.DataExisted<T_User>(p => p.Phone == input.Phone, true);
            if (!accountExist)
            {
                return ServiceResult.IsFailure(_stringLocalizer["UserNotRegister"].Value);
            }
            // 验证验证码是否匹配
            ServiceResult phoneCodeResult = await _captchaService.PhoneCodeVerify(VerificationCodeTypeEnum.Login, input.Phone, input.VerifyCode);
            if (!phoneCodeResult.Success)
            {
                return phoneCodeResult;
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 注册验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> RegisteredVerify(RegisteredInput input)
        {
            // 验证邀请码是否正确
            (long Id, string Code) tenandInfo = await _frontDeskOAuthDao.GetIdByInviteCode(input.InviteCode);
            if (tenandInfo.Id.Equals(0))
            {
                return ServiceResult.IsFailure(_stringLocalizer["InviteCodeError"].Value);
            }
            // 判断账号在该平台下是否已被注册
            bool hasRegiste = await _frontDeskOAuthDao.DataExisted<T_User>(p => p.Phone == input.Phone && p.TenantId == tenandInfo.Id, true);
            if (hasRegiste)
            {
                return ServiceResult.IsFailure(_stringLocalizer["UserExisted"].Value);
            }
            // 验证验证码是否匹配
            ServiceResult codeVerifyResult = await _captchaService.PhoneCodeVerify(VerificationCodeTypeEnum.Register, input.Phone, input.VerifyCode);
            if (!codeVerifyResult.Success)
            {
                return codeVerifyResult;
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 忘记密码验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> ForgotPasswordVerify(ForgotPasswordInput input)
        {
            // 判断账号是否注册
            bool accountExist = await _frontDeskOAuthDao.DataExisted<T_User>(p => p.Phone == input.Phone, true);
            if (!accountExist)
            {
                return ServiceResult.IsFailure(_stringLocalizer["UserNotRegister"].Value);
            }
            // 验证验证码是否匹配
            ServiceResult codeVerifyResult = await _captchaService.PhoneCodeVerify(VerificationCodeTypeEnum.ForgetPwd, input.Phone, input.VerifyCode);
            if (!codeVerifyResult.Success)
            {
                return codeVerifyResult;
            }
            return ServiceResult.Successed();
        }
        #endregion

    }
}
