using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SharedLibrary.Enums;

namespace WebApi_Offcial.Controllers
{
    /// <summary>
    /// Swagger分组特性
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
        /// <param name="swaggerGroupEnum"></param>
        public ApiDescriptionAttribute(SwaggerGroupEnum swaggerGroupEnum)
        {
            GroupName = swaggerGroupEnum.ToString();
        }
    }
}
