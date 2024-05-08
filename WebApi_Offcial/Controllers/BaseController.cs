using Microsoft.AspNetCore.Mvc;

namespace WebApi_Offcial.Controllers
{
    /// <summary>
    /// 权限验证控制器
    /// </summary>
    /// <remarks>Controller继承此类实现菜单权限验证</remarks>
   // [TypeFilter(typeof(MenusAndButtonsAuthorizationFilter))]
    public class BaseController : ControllerBase
    {
    }
}
