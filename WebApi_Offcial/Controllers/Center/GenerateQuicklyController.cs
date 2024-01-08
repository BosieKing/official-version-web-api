using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Enums;
using WebApi_Offcial.ConfigureServices;

namespace WebApi_Offcial.Controllers.Center
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [ApiController]
    [Route("GenerateQuickly")]
    [ApiDescription(SwaggerGroupEnum.Center)]
    public class GenerateQuicklyController : ControllerBase
    {
        /// <summary>
        /// 初始化基础文件
        /// </summary>
        /// <param name="classNamePrefix">类名：TestManage</param>
        /// <param name="chinesesName">中文描述:测试管理</param>
        /// <param name="tableName">主表：T_Test</param>
        /// <param name="swaggerGroupEnumName">位置：1业务后台 2前台 3中间服务 4系统管理</param>
        /// <returns></returns>
        [HttpPost("Create")]
        [AllowAnonymous]
        public string Create(string classNamePrefix, string chinesesName, string tableName, SwaggerGroupEnum swaggerGroupEnumName)
        {
            var input = new GenerateQuicklyInput(classNamePrefix, chinesesName, tableName, swaggerGroupEnumName);
            var tool = new GenerateQuicklyTool(input);
            tool.Generate();
            return "生成成功";
        }


        /// <summary>
        /// 生成输入类
        /// </summary>
        /// <param name="inputType">输入类型 Add Update GetPage</param>
        /// <param name="classNamePrefix">类名：TestManage</param>
        /// <param name="chinesesName">中文描述:测试管理</param>
        /// <param name="tableName">主表：T_Test</param>
        /// <param name="swaggerGroupEnumName">位置：1业务后台 2前台 3中间服务 4系统管理</param>
        /// <returns></returns>
        [HttpPost("CreateByTable")]
        [AllowAnonymous]
        public string CreateByTable(string inputType, string classNamePrefix, string chinesesName, string tableName, SwaggerGroupEnum swaggerGroupEnumName)
        {
            var input = new GenerateQuicklyInput(classNamePrefix, chinesesName, tableName, swaggerGroupEnumName);
            var tool = new GenerateQuicklyTool(input);
            tool.GenerateByTable(inputType);
            return "生成成功";
        }
    }
}
