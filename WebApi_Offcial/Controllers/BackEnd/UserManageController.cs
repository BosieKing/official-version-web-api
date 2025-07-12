using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.UserManage;
using Service.BackEnd.UserManage;
using SharedLibrary.Enums;
using WebApi_Offcial.ActionFilters.BackEnd;

namespace WebApi_Offcial.Controllers.BackEnd
{
    /// <summary>
    /// 用户管理控制层
    /// </summary>
    [ApiController]
    [Route("UserManage")]
    [ApiDescription(SwaggerGroupEnum.BackEnd)]
    [ServiceFilter(typeof(UserManageActionFilter))]
    public class UserManageController : BaseController
    {
        #region 构造函数
        private readonly IUserManageService _userManageService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserManageController(IUserManageService userManageService)
        {
            _userManageService = userManageService;
        }
        #endregion     
    }
}
