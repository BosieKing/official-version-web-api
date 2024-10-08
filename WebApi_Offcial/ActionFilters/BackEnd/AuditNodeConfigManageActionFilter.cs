using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.AuditNodeConfigManage;
using UtilityToolkit.Utils;
using Model.Repositotys.BasicData;
using UtilityToolkit.Helpers;
using IDataSphere.Interfaces.BackEnd;

namespace WebApi_Offcial.ActionFilters.BackEnd
{
    /// <summary>
    /// 审核流程配置表过滤切面
    /// </summary>
    public class AuditNodeConfigManageActionFilter : IAsyncActionFilter
    {
        #region 构造函数
        private readonly IAuditNodeConfigManageDao _auditNodeConfigManageDao;
        private readonly IStringLocalizer<UserTips> _stringLocalizer;

        public AuditNodeConfigManageActionFilter(IAuditNodeConfigManageDao auditNodeConfigManageDao, IStringLocalizer<UserTips> stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;
            this._auditNodeConfigManageDao = auditNodeConfigManageDao;
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
                case "addauditnodeconfig":
                    serviceResult = await AddAuditNodeConfigVerify((AddAuditNodeConfigInput)dic["input"]);
                    break;
                case "updateauditnodeconfig":
                    serviceResult = await UpdateAuditNodeConfigVerify((UpdateAuditNodeConfigInput)dic["input"]);
                    break;
                case "deleteauditnodeconfig":
                    serviceResult = await DeleteAuditNodeConfigVerify((IdInput)dic["input"]);
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
        private async Task<ServiceResult> AddAuditNodeConfigVerify(AddAuditNodeConfigInput input)
        {
            bool dataExisted = await _auditNodeConfigManageDao.SingleDataExisted<T_AuditNodeConfig>(p => p.Name == input.Name);
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
        private async Task<ServiceResult> UpdateAuditNodeConfigVerify(UpdateAuditNodeConfigInput input)
        {
            bool idExisted = await _auditNodeConfigManageDao.IdExisted<T_AuditNodeConfig>(input.Id);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataNotExist"].Value);
            }
            bool dataExisted = await _auditNodeConfigManageDao.SingleDataExisted<T_AuditNodeConfig>(p => p.Id != input.Id && p.Name == input.Name);
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
        private async Task<ServiceResult> DeleteAuditNodeConfigVerify(IdInput input)
        {
            bool idExisted = await _auditNodeConfigManageDao.IdExisted<T_AuditNodeConfig>(input.Id);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataNotExist"].Value);
            }
            return ServiceResult.Successed();
        }      
        #endregion
    }
}






