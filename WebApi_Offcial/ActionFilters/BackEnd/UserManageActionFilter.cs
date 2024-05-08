using IDataSphere.Interfaces.BackEnd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.UserManage;
using Model.Repositotys.BasicData;
using Model.Repositotys.Service;
using UtilityToolkit.Helpers;

namespace WebApi_Offcial.ActionFilters.BackEnd
{
    /// <summary>
    /// 用户管理过滤切面
    /// </summary>
    public class UserManageActionFilter : IAsyncActionFilter
    {
        #region 构造函数
        private readonly IUserManageDao _userManageDao;
        private readonly IStringLocalizer<UserTips> _stringLocalizer;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userManageDao"></param>
        /// <param name="stringLocalizer"></param>
        public UserManageActionFilter(IUserManageDao userManageDao, IStringLocalizer<UserTips> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManageDao = userManageDao;
        }
        #endregion

        #region 过滤切面
        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string actionName = context.RouteData.Values["action"].ToString().ToLower();
            Dictionary<string, object> dic = (Dictionary<string, object>)context.ActionArguments;
            ServiceResult serviceResult = null;
            switch (actionName)
            {
                case "adduser":
                    serviceResult = await AddUserVerify((AddUserInput)dic["input"]);
                    break;
                case "adduserrole":
                    serviceResult = await AddUserRoleVerify((AddUserRoleInput)dic["input"]);
                    break;
                case "updateuser":
                    serviceResult = await UpdateUserVerify((UpdateUserInput)dic["input"]);
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
        /// 新增用户
        /// </summary>
        /// <returns></returns>
        private async Task<ServiceResult> AddUserVerify(AddUserInput input)
        {
            // 判断账号是否注册
            bool accountExist = await _userManageDao.DataExisted<T_User>(p => p.Phone == input.Phone);
            if (accountExist)
            {
                return ServiceResult.IsFailure(_stringLocalizer["PhoneExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 用户角色绑定
        /// </summary>
        /// <returns></returns>
        private async Task<ServiceResult> AddUserRoleVerify(AddUserRoleInput input)
        {
            bool accountExist = await _userManageDao.IdExisted<T_User>(input.UserId);
            if (!accountExist)
            {
                return ServiceResult.IsFailure(_stringLocalizer["UserNotRegister"].Value);
            }
            if (input.RoleIds.Count() > 0)
            {
                bool roIdsExist = await _userManageDao.DataExisted<T_Role>(p => input.RoleIds.Contains(p.Id));
                if (!roIdsExist)
                {
                    return ServiceResult.IsFailure(_stringLocalizer["RoleNotExist"].Value);
                }
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <returns></returns>
        private async Task<ServiceResult> UpdateUserVerify(UpdateUserInput input)
        {
            // 判断账号是否注册
            bool accountExist = await _userManageDao.DataExisted<T_User>(p => p.Phone == input.Phone && p.Id != input.Id);
            if (accountExist)
            {
                return ServiceResult.IsFailure(_stringLocalizer["PhoneExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        #endregion
    }
}






