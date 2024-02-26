using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Enums;
using WebApi_Offcial.ConfigFiles.GenerateQuicklyTemplate;
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
        /// <returns></returns>
        [HttpPost("Create")]
        [AllowAnonymous]
        public ActionResult<string> Create
            (
            string ClassNamePrefix,
            string ChinesesName,
            string TableName,
            SwaggerGroupEnum Address,
            bool IsNeedMenuControl,
            bool IsNeedHttpContext,
            List<ActionGenerateQuicklyInput> ActionDic
            )
        {
            ControllerGenerateQuicklyInput input = new ControllerGenerateQuicklyInput(ClassNamePrefix,ClassNamePrefix,TableName, Address, IsNeedMenuControl,IsNeedHttpContext);
            input.ActionDic = ActionDic;
            GenerateQuicklyTool generateQuicklyTool = new GenerateQuicklyTool();
            generateQuicklyTool.CreateController(input);
            return "生成成功";
        }
    }
}
