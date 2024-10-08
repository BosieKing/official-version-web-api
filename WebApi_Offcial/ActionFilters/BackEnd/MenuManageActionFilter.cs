using IDataSphere.Interfaces.BackEnd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.MenuManage;
using Model.Repositotys.BasicData;
using UtilityToolkit.Helpers;

namespace WebApi_Offcial.ActionFilters.BackEnd
{
    /// <summary>
    /// 菜单管理过滤器
    /// </summary>
    public class MenuManageActionFilter : IAsyncActionFilter
    {
        #region 构造函数
        private readonly IMenuManageDao _menuDao;
        private readonly IStringLocalizer<UserTips> _stringLocalizer;
        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuManageActionFilter(IStringLocalizer<UserTips> stringLocalizer, IMenuManageDao menuDao)
        {
            _stringLocalizer = stringLocalizer;
            _menuDao = menuDao;
        }

        #endregion

        #region 切面
        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="context"></param>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string actionName = context.RouteData.Values["action"].ToString().ToLower();
            Dictionary<string, object> dic = (Dictionary<string, object>)context.ActionArguments;
            ServiceResult serviceResult = null;
            switch (actionName)
            {
                case "adddirectory":
                    serviceResult = await AddDirectoryVerify((AddDirectoryInput)dic["input"]);
                    break;
                case "addmenu":
                    serviceResult = await AddMenuVerify((AddMenuInput)dic["input"]);
                    break;
                case "addmenubutton":
                    serviceResult = await AddMenuButtonVerify((AddMenuButtonInput)dic["input"]);
                    break;
                case "updatedirectory":
                    serviceResult = await UpdateDirectoryVerify((UpdateDirectoryInput)dic["input"]);
                    break;
                case "updatemenu":
                    serviceResult = await UpdateMeunVerify((UpdateMeunInput)dic["input"]);
                    break;
                case "updatemenubutton":
                    serviceResult = await UpdateMenuButtonVerify((UpdateMenuButtonInput)dic["input"]);
                    break;
                case "deletedirectory":
                    serviceResult = await DeleteDirectoryVerify((IdInput)dic["input"]);
                    break;
                case "deletemenu":
                    serviceResult = await DeleteMenuVerify((IdInput)dic["input"]);
                    break;
                case "deletemenubutton":
                    serviceResult = await DeleteMenuButtonVerify((IdInput)dic["input"]);
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
        private async Task<ServiceResult> AddDirectoryVerify(AddDirectoryInput input)
        {
            bool dataExisted = await _menuDao.SingleDataExisted<T_Directory>(p => p.Name == input.Name && p.BrowserPath == input.BrowserPath);
            if (dataExisted)
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
        private async Task<ServiceResult> AddMenuVerify(AddMenuInput input)
        {
            bool existed = await _menuDao.IdExisted<T_Directory>(input.DirectoryId);
            if (!existed)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DirectoryNotExist"].Value);
            }
            bool result = await _menuDao.SingleDataExisted<T_Menu>(p => p.Name == input.Name && p.BrowserPath == input.BrowserPath && p.ControllerRouter == input.Router);
            if (result)
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
        private async Task<ServiceResult> AddMenuButtonVerify(AddMenuButtonInput input)
        {
            bool idExisted = await _menuDao.IdExisted<T_Menu>(input.MenuId);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["MenuNotExist"].Value);
            }
            bool dataExisted = await _menuDao.SingleDataExisted<T_MenuButton>(p => p.Name == input.Name && p.ActionName == input.ActionName);
            if (dataExisted)
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
        private async Task<ServiceResult> UpdateDirectoryVerify(UpdateDirectoryInput input)
        {
            bool nameExisted = await _menuDao.SingleDataExisted<T_Directory>(p => p.Name == input.Name && p.BrowserPath == input.BrowserPath && p.Id != input.Id);
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
        private async Task<ServiceResult> UpdateMeunVerify(UpdateMeunInput input)
        {
            bool idExisted = await _menuDao.IdExisted<T_Directory>(input.DirectoryId);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DirectoryNotExist"].Value);
            }
            bool dataExisted = await _menuDao.SingleDataExisted<T_Menu>(p => p.Name == input.Name && p.BrowserPath == input.BrowserPath && p.ControllerRouter == input.Router && p.Id != input.Id);
            if (dataExisted)
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
        private async Task<ServiceResult> UpdateMenuButtonVerify(UpdateMenuButtonInput input)
        {
            bool idExisted = await _menuDao.IdExisted<T_Menu>(input.MenuId);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["MenuNotExist"].Value);
            }
            bool dataExisted = await _menuDao.SingleDataExisted<T_MenuButton>(p => p.Name == input.Name && p.ActionName == input.ActionName && p.Id != input.Id);
            if (dataExisted)
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
        private async Task<ServiceResult> DeleteDirectoryVerify(IdInput input)
        {
            // 验证目录是否已经和菜单解除绑定
            bool unbound = await _menuDao.SingleDataExisted<T_Menu>(p => p.DirectoryId == input.Id);
            if (unbound)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DirectoryNotUnbound"].Value);
            }
            bool idExisted = await _menuDao.IdExisted<T_Directory>(input.Id);
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
        private async Task<ServiceResult> DeleteMenuVerify(IdInput input)
        {
            bool idExisted = await _menuDao.IdExisted<T_Menu>(input.Id);
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
        private async Task<ServiceResult> DeleteMenuButtonVerify(IdInput input)
        {
            bool idExisted = await _menuDao.IdExisted<T_MenuButton>(input.Id);
            if (!idExisted)
            {
                return ServiceResult.IsFailure(_stringLocalizer["DataNotExist"].Value);
            }
            return ServiceResult.Successed();
        }
        #endregion
    }
}
