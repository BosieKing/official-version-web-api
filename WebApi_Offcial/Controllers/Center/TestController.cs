using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Repositotys.Log;
using SharedLibrary.Enums;
using StackExchange.Profiling;
using UtilityToolkit.Helpers;

namespace WebApi_Offcial.Controllers.Center
{
    /// <summary>
    /// 测试专用控制器
    /// </summary>
    [Route("Test")]
    [ApiController]
    [ApiDescription(SwaggerGroupEnum.Center)]
    [AllowAnonymous]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 抛出异常测试队列信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("throwEx")]
        public async Task<ActionResult<dynamic>> throwEx()
        {
            var html = MiniProfiler.Current.RenderIncludes(HttpContext); 
            return html.Value;
        }

        /// <summary>
        /// 获取队列长度
        /// </summary>
        /// <returns></returns>
        [HttpGet("getQueueCount")]
        public async Task<ActionResult<long>> getQueueCount()
        {
            return QueueSingletonHelper<TL_ErrorLog>.Instance.Count();
        }
    }
}
