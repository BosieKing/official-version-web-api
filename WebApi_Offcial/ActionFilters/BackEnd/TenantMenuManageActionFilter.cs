using IDataSphere.Interfaces.BackEnd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.TenantMenuManage;
using Model.Repositotys.BasicData;
using UtilityToolkit.Helpers;

namespace WebApi_Offcial.ActionFilters.BackEnd
{
    /// <summary>
    /// 租户菜单管理过滤切面
    /// </summary>
    public class TenantMenuManageActionFilter : IAsyncActionFilter
    {
        #region 构造函数
        private readonly ITenantMenuManageDao _tenantMenuManageDao;
        private readonly IStringLocalizer<UserTips> _stringLocalizer;

        public TenantMenuManageActionFilter(ITenantMenuManageDao tenantMenuManageDao, IStringLocalizer<UserTips> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _tenantMenuManageDao = tenantMenuManageDao;
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
                case "addtenantdirectory":
                    serviceResult = await AddTenantDirectoryVerify((AddTenantDirectoryInput)dic["input"]);
                    break;
                case "addtenantmenu":
                    serviceResult = await AddTenantMenuVerify((AddTenantMenuInput)dic["input"]);
                    break;
                case "addtenantmenubutton":
                    serviceResult = await AddTenantMenuButtonVerify((AddTenantMenuButtonInput)dic["input"]);
                    break;
                case "updatetenantdirectory":
                    serviceResult = await UpdateTenantDirectoryVerify((UpdateTenantDirectoryInput)dic["input"]);
                    break;
                case "updatetenantmenu":
                    serviceResult = await UpdateTenantMeunVerify((UpdateTenantMenuInput)dic["input"]);
                    break;
                case "updatetenantmenubutton":
                    serviceResult = await UpdateTenantMenuButtonVerify((UpdateTenantMenuButtonInput)dic["input"]);
                    break;
                case "deletetenantdirectory":
                    serviceResult = await DeleteTenantDirectoryVerify((IdInput)dic["input"]);
                    break;
                case "deletetenantmenu":
                    serviceResult = await DeleteTenantMenuVerify((IdInput)dic["input"]);
                    break;
                case "deletetenantmenubutton":
                    serviceResult = await DeleteTenantMenuButtonVerify((IdInput)dic["input"]);
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
        /// 增加目录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> AddTenantDirectoryVerify(AddTenantDirectoryInput input)
        {
            bool nameExisted = await _tenantMenuManageDao.DataExisted<T_TenantDirectory>(p => p.Name == input.Name && p.BrowserPath == input.BrowserPath);
            if (nameExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 增加菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> AddTenantMenuVerify(AddTenantMenuInput input)
        {
            bool idExisted = await _tenantMenuManageDao.IdExisted<T_TenantDirectory>(input.DirectoryId);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DirectoryNotExist"].Value);
            }
            bool nameExisted = await _tenantMenuManageDao.DataExisted<T_TenantMenu>(p => p.Name == input.Name && p.BrowserPath == input.BrowserPath && p.Router == input.Router);
            if (nameExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> AddTenantMenuButtonVerify(AddTenantMenuButtonInput input)
        {
            bool idExisted = await _tenantMenuManageDao.IdExisted<T_TenantMenu>(input.MenuId);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["MenuNotExist"].Value);
            }
            bool nameExisted = await _tenantMenuManageDao.DataExisted<T_TenantMenuButton>(p => p.Name == input.Name && p.ActionName == input.ActionName);
            if (nameExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 修改目录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> UpdateTenantDirectoryVerify(UpdateTenantDirectoryInput input)
        {
            bool nameExisted = await _tenantMenuManageDao.DataExisted<T_TenantDirectory>(p => p.Name == input.Name && p.BrowserPath == input.BrowserPath && p.Id != input.Id);
            if (nameExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> UpdateTenantMeunVerify(UpdateTenantMenuInput input)
        {
            bool idExisted = await _tenantMenuManageDao.IdExisted<T_TenantDirectory>(input.DirectoryId);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DirectoryNotExist"].Value);
            }
            bool nameExisted = await _tenantMenuManageDao.DataExisted<T_TenantMenu>(p => p.Name == input.Name && p.BrowserPath == input.BrowserPath && p.Router == input.Router && p.Id != input.Id);
            if (nameExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> UpdateTenantMenuButtonVerify(UpdateTenantMenuButtonInput input)
        {
            bool idExisted = await _tenantMenuManageDao.IdExisted<T_TenantMenu>(input.MenuId);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["MenuNotExist"].Value);
            }
            bool nameExisted = await _tenantMenuManageDao.DataExisted<T_TenantMenuButton>(p => p.Name == input.Name && p.ActionName == input.ActionName && p.Id != input.Id);
            if (nameExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> DeleteTenantDirectoryVerify(IdInput input)
        {
            bool unbound = await _tenantMenuManageDao.DataExisted<T_TenantMenu>(p => p.DirectoryId == input.Id);
            if (unbound)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DirectoryNotUnbound"].Value);
            }
            bool idExisted = await _tenantMenuManageDao.IdExisted<T_TenantDirectory>(input.Id);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataNotExist"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> DeleteTenantMenuVerify(IdInput input)
        {
            bool unboundByRole = await _tenantMenuManageDao.DataExisted<T_RoleMenu>(p => p.MenuId == input.Id);
            if (unboundByRole)
            {
                return ServiceResult.IsFailure(_stringLocalizer["MenuNotUnboundByRole"].Value);
            }
            bool idExisted = await _tenantMenuManageDao.IdExisted<T_TenantMenu>(input.Id);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataNotExist"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> DeleteTenantMenuButtonVerify(IdInput input)
        {
            bool idExisted = await _tenantMenuManageDao.IdExisted<T_TenantMenuButton>(input.Id);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataNotExist"].Value);
            }
            return ServiceResult.Successed();
        }
        #endregion
    }
}






