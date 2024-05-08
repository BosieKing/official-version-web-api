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
        /// <param name="tableSource">主表位置：1基础数据 2日志 3中间服务 4Tarot</param>
        /// <param name="swaggerGroupEnumName">位置：1业务后台 2前台 3中间服务 4系统管理 5Tarot</param>
        /// <returns></returns>
        [HttpPost("Create")]
        [AllowAnonymous]
        public string Create(string classNamePrefix, string chinesesName, string tableName, TableGroupEnum tableSource, SwaggerGroupEnum swaggerGroupEnumName)
        {
            GenerateQuicklyInput input = new GenerateQuicklyInput(classNamePrefix, chinesesName, tableName, swaggerGroupEnumName, tableSource);
            GenerateQuicklyTool tool = new GenerateQuicklyTool(input);
            tool.Generate();
            return "生成成功";
        }
    }
}
