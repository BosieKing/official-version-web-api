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
            bool isSuperManage = bool.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.IS_SUPERMANAGE)?.Value ?? "false");
            if (isSuperManage)
            {
                return Task.CompletedTask;
            }
            string roleIds = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.ROLE_ID)?.Value ?? "";
            long tenantId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.TENANT_ID)?.Value ?? "0");
            // 权限处理
            string actioName = context.RouteData.Values["action"].ToString().ToLower();
            string controllerName = context.RouteData.Values["controller"].ToString().ToLower();
            if (!RedisMulititionHelper.HasRole(roleIds.Split(","), controllerName, tenantId))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                var result = DataResponseModel.IsFailure("无权访问");
                // 管道短路
                context.Result = new JsonResult(result);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
