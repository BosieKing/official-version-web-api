using IDataSphere.Interfaces.BackEnd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.RoleManage;
using Model.Repositotys;
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

        #region 切面
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
                case "addrole":
                    serviceResult = await AddRoleVerify((AddRoleInput)dic["input"]);
                    break;
                case "addrolemenu":
                    serviceResult = await AddRoleMenuVerify((AddRoleMenuInput)dic["input"]);
                    break;
                case "updaterole":
                    serviceResult = await UpdateRoleVerify((UpdateRoleInput)dic["input"]);
                    break;
                case "deleterole":
                    serviceResult = await DeleteRoleVerify((IdInput)dic["input"]);
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
        /// 新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> AddRoleVerify(AddRoleInput input)
        {
            bool nameExistd = await _roleManageDao.DataExisted<T_Role>(p => p.Name == input.Name);
            if (nameExistd)
            {
                return ServiceResult.IsFailure(_stringLocalizer["NameExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 绑定菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> AddRoleMenuVerify(AddRoleMenuInput input)
        {
            bool idExistd = await _roleManageDao.IdExisted<T_Role>(input.RoleId);
            if (!idExistd)
            {
                return ServiceResult.IsFailure(_stringLocalizer["RoleNotExist"].Value);
            }
            bool menuExistd = await _roleManageDao.DataExisted<T_TenantMenu>(p => input.MenuIds.Contains(p.Id));
            if (!menuExistd)
            {
                return ServiceResult.IsFailure(_stringLocalizer["MenuNotExist"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> UpdateRoleVerify(UpdateRoleInput input)
        {
            bool nameExistd = await _roleManageDao.DataExisted<T_Role>(p => p.Name == input.Name && p.Id != input.Id);
            if (nameExistd)
            {
                return ServiceResult.IsFailure(_stringLocalizer["NameExisted"].Value);
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<ServiceResult> DeleteRoleVerify(IdInput input)
        {
            bool unbound = await _roleManageDao.DataExisted<T_UserRole>(p => p.RoleId == input.Id);
            if (unbound)
            {
                return ServiceResult.IsFailure(_stringLocalizer["RoleNotUnboundByUser"].Value);
            }
            return ServiceResult.Successed();
        }
        #endregion
    }
}
