using IDataSphere.Interfaces.BackEnd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Model.Commons.Domain;
using UtilityToolkit.Helpers;

namespace WebApi_Offcial.ActionFilters.BackEnd
{
    /// <summary>
    /// 角色管理过滤切面
    /// </summary>
    public class RoleManageActionFilter : IAsyncActionFilter
    {
        #region 构造函数
        private readonly IRoleManageDao _roleManageDao;
        private readonly IStringLocalizer<UserTips> _stringLocalizer;

        public RoleManageActionFilter(IRoleManageDao roleManageDao, IStringLocalizer<UserTips> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _roleManageDao = roleManageDao;
        }
        #endregion

        #region
        /// <summary>
        /// 过滤切面
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
        private async Task<ServiceResult> AddVerify()
        {

            return ServiceResult.Successed();
        }
        #endregion
    }
}
