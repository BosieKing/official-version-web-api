using IDataSphere.Interfaces.FronDesk;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Model.Commons.Domain;
using Model.DTOs.FronDesk.UserInfoManage;
using Model.Repositotys.Service;
using Service.Center.Captcha;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;

namespace WebApi_Offcial.ActionFilters.FrontDesk
{
    /// <summary>
    /// 用户中心管理切面
    /// </summary>
    public class UserInfoManageActionFilter : IAsyncActionFilter
    {
        #region 构造函数及参数  
        private readonly IUserInfoManageDao _userInfoDao;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<UserTips> _stringLocalizer;
        private readonly ICaptchaService _captchaService;
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserInfoManageActionFilter(IUserInfoManageDao userInfoDao,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<UserTips> stringLocalizer,
            ICaptchaService captchaService)
        {
            _userInfoDao = userInfoDao;
            _httpContextAccessor = httpContextAccessor;
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
        /// <exception cref="NotImplementedException"></exception>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string actionName = context.RouteData.Values["action"].ToString().ToLower();
            Dictionary<string, object> dic = (Dictionary<string, object>)context.ActionArguments;
            ServiceResult serviceResult = null;
            switch (actionName)
            {
                case "completeuserinfo":
                    serviceResult = await CompleteUserInfoVerify((CompleteUserInfoInput)dic["input"]);
                    break;
                case "updatepassword":
                    serviceResult = await UpdatePasswordVerify((UpdatePasswordInput)dic["input"]);
                    break;
                case "updatepasswordbycode":
                    serviceResult = await UpdatePasswordByCodeVerify((UpdatePasswordByCodeInput)dic["input"]);
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

        #region 验证方法
        /// <summary>
        /// 验证完善资料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> CompleteUserInfoVerify(CompleteUserInfoInput input)
        {
            // 根据id找到用户信息
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            bool result = await _userInfoDao.IdExisted<T_User>(userId);
            if (!result)
            {
                return ServiceResult.IsFailure(_stringLocalizer["UserNotRegister"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 验证通过原密码修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> UpdatePasswordVerify(UpdatePasswordInput input)
        {
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            bool result = await _userInfoDao.DataExisted<T_User>(p => p.Id == userId && p.Password == input.OldPassword);
            if (!result)
            {
                return ServiceResult.IsFailure(_stringLocalizer["OldPasswordError"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 验证通过验证码修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> UpdatePasswordByCodeVerify(UpdatePasswordByCodeInput input)
        {
            long userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID).Value);
            string phone = await _userInfoDao.GetFirstStringTypeField<T_User>(p => p.Id == userId, nameof(T_User.Phone)); ;
            // 验证验证码是否匹配
            var result = await _captchaService.PhoneCodeVerify(VerificationCodeTypeEnum.UpdatePwd, phone, input.VerifyCode);
            if (!result.Success)
            {
                return result;
            }
            return ServiceResult.Successed();
        }
        #endregion
    }
}
