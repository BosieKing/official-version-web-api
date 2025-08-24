using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi_Offcial.ActionFilters.BackEnd
{
    /// <summary>
    /// 后台权限管理AOP
    /// </summary>
    public class BackOAuthActionFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           
        }
    }
}
