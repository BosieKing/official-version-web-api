using IDataSphere.Interfaces.BackEnd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.TenantManage;
using UtilityToolkit.Helpers;

namespace WebApi_Offcial.ActionFilters.BackEnd
{
    /// <summary>
    /// 租户管理验证切面
    /// </summary>
    public class TenantManageActionFilter : IAsyncActionFilter
    {
        #region 构造函数及参数
        private readonly ITenantManageDao _tenantDao;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<UserTips> _stringLocalizer;
        public TenantManageActionFilter(ITenantManageDao tenantDao, IHttpContextAccessor httpContextAccessor, IStringLocalizer<UserTips> stringLocalizer)
        {
            _tenantDao = tenantDao;
            _httpContextAccessor = httpContextAccessor;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region 切面
        /// <summary>
        /// 执行前
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
                case "addtenant":
                    serviceResult = await AddTenantVerify((AddTenantInput)dic["input"]);
                    break;
                case "updatetenant":
                    serviceResult = await UpdateTenantVerify((UpdateTenantInput)dic["input"]);
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
        /// 验证添加租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> AddTenantVerify(AddTenantInput input)
        {
            // 判断租户的唯一编码是否已存在
            bool codeExisted = await _tenantDao.CodeExist(input.Code);
            if (codeExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["TenantCodeExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 验证修改租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> UpdateTenantVerify(UpdateTenantInput input)
        {
            // 判断租户的唯一编码是否已存在
            bool codeExisted = await _tenantDao.CodeExist(input.Code, input.Id);
            if (codeExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["TenantCodeExisted"].Value);
            }
            return ServiceResult.Successed();
        }
        #endregion
    }
}
