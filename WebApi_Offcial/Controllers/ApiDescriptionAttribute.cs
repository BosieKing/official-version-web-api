using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SharedLibrary.Enums;

namespace WebApi_Offcial.Controllers
{
    /// <summary>
    /// 标记此特性可实现Swagger分组 
    /// </summary>
    public class ApiDescriptionAttribute : Attribute, IApiDescriptionGroupNameProvider
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="swaggerGroupConst">分组名称</param>
        public ApiDescriptionAttribute(SwaggerGroupEnum swaggerGroupConst)
        {
            GroupName = swaggerGroupConst.ToString();
        }
    }
}
