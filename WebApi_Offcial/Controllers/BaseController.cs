using Microsoft.AspNetCore.Mvc;
using WebApi_Offcial.ActionFilters;

namespace WebApi_Offcial.Controllers
{
    /// <summary>
    /// 自定义所有控制器的父类
    /// </summary>
    [TypeFilter(typeof(MenusAndButtonsAuthorizationFilter))]
    public class BaseController : ControllerBase
    {
    }
}
