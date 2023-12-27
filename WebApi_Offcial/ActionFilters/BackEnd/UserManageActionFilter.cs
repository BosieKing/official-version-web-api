using BusinesLogic.BackEnd.UserManage.Dto;
using IDataSphere.Interface.BackEnd.UserManage;
using IDataSphere.Repositoty;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using SharedLibrary.Models.DomainModels;
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
            DataResponseModel serviceResult = null;
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

        #region 验证方法     
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns></returns>
        private async Task<DataResponseModel> AddUserVerify(AddUserInput input)
        {
            // 判断账号是否注册
            bool accountExist = await _userManageDao.DataExisted(p => p.Phone == input.Phone);
            if (accountExist)
            {
                return DataResponseModel.IsFailure(_stringLocalizer["PhoneExisted"].Value);
            }
            return DataResponseModel.Successed();
        }

        /// <summary>
        /// 用户角色绑定
        /// </summary>
        /// <returns></returns>
        private async Task<DataResponseModel> AddUserRoleVerify(AddUserRoleInput input)
        {
            bool accountExist = await _userManageDao.IdExisted(input.UserId);
            if (!accountExist)
            {
                return DataResponseModel.IsFailure(_stringLocalizer["UserNotRegister"].Value);
            }
            if (input.RoleIds.Count() > 0)
            {
                bool roidsExist = await _userManageDao.DataExisted<T_Role>(p => input.RoleIds.Contains(p.Id));
                if (!roidsExist)
                {
                    return DataResponseModel.IsFailure(_stringLocalizer["RoleNotExist"].Value);
                }
            }
            return DataResponseModel.Successed();
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <returns></returns>
        private async Task<DataResponseModel> UpdateUserVerify(UpdateUserInput input)
        {
            // 判断账号是否注册
            bool accountExist = await _userManageDao.DataExisted(p => p.Phone == input.Phone && p.Id != input.Id);
            if (accountExist)
            {
                return DataResponseModel.IsFailure(_stringLocalizer["PhoneExisted"].Value);
            }
            return DataResponseModel.Successed();
        }

        #endregion
    }
}






