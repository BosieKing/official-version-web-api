using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.AuditTypeManage;
using UtilityToolkit.Utils;
using Model.Repositotys.BasicData;
using UtilityToolkit.Helpers;
using IDataSphere.Interfaces.BackEnd;

namespace WebApi_Offcial.ActionFilters.BackEnd
{
    /// <summary>
    /// 审核角色类型配置表过滤切面
    /// </summary>
    public class AuditTypeManageActionFilter : IAsyncActionFilter
    {
        #region 构造函数
        private readonly IAuditTypeManageDao _auditTypeManageDao;
        private readonly IStringLocalizer<UserTips> _stringLocalizer;

        public AuditTypeManageActionFilter(IAuditTypeManageDao auditTypeManageDao, IStringLocalizer<UserTips> stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;
            this._auditTypeManageDao = auditTypeManageDao;
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
                case "addaudittype":
                    serviceResult = await AddAuditTypeVerify((AddAuditTypeInput)dic["input"]);
                    break;
                case "updateaudittype":
                    serviceResult = await UpdateAuditTypeVerify((UpdateAuditTypeInput)dic["input"]);
                    break;
                case "deleteaudittype":
                    serviceResult = await DeleteAuditTypeVerify((IdInput)dic["input"]);
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
        /// 新增
        /// </summary>
        /// <returns></returns>
        private async Task<ServiceResult> AddAuditTypeVerify(AddAuditTypeInput input)
        {
            bool dataExisted = await _auditTypeManageDao.SingleDataExisted<T_AuditType>(p => p.Name == input.Name);
            if (dataExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        private async Task<ServiceResult> UpdateAuditTypeVerify(UpdateAuditTypeInput input)
        {
            bool idExisted = await _auditTypeManageDao.IdExisted<T_AuditType>(input.Id);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataNotExist"].Value);
            }
            bool dataExisted = await _auditTypeManageDao.SingleDataExisted<T_AuditType>(p => p.Id != input.Id && p.Name == input.Name);
            if (dataExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        private async Task<ServiceResult> DeleteAuditTypeVerify(IdInput input)
        {
            bool idExisted = await _auditTypeManageDao.IdExisted<T_AuditType>(input.Id);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataNotExist"].Value);
            }
            return ServiceResult.Successed();
        }      
        #endregion
    }
}






