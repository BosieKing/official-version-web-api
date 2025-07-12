using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model.Commons.Domain;
using SharedLibrary.Consts;
using System.Net;
using UtilityToolkit.Helpers;

namespace WebApi_Offcial.ActionFilters
{
    /// <summary>
    /// 菜单权限控制
    /// </summary>
    public class MenusAndButtonsAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public MenusAndButtonsAuthorizationFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="context"></param>
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            return Task.CompletedTask;
            // 判断是否是超管
            bool isSuperManage = bool.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.IS_SUPERMANAGE)?.Value ?? "false");
            if (isSuperManage)
            {
                return Task.CompletedTask;
            }
            else
            {
                string roleIds = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.ROLE_IDs)?.Value ?? "";              
                // 判断有无权限访问此接口
                string actioName = context.RouteData.Values["action"].ToString().ToLower();
                string controllerName = context.RouteData.Values["controller"].ToString().ToLower();
                if (!RedisMulititionHelper.HasRole(roleIds.Split(","), controllerName))
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    // 管道短路
                    context.Result = new JsonResult(ServiceResult.IsFailure("无权访问"));
                    return Task.CompletedTask;
                }
            }
            return Task.CompletedTask;
        }
    }
}
