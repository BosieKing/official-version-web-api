using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Repositotys.Log;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SharedLibrary.Enums;
using StackExchange.Profiling;
using System.Text;
using System.Threading.Channels;
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
        private readonly IConnection _rabbitMQConn;
        public TestController(IConnection rabbitMQConn)
        {
            this._rabbitMQConn = rabbitMQConn;
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Seed")]
        public ActionResult<dynamic> Seed(string mes)
        {
            var model = _rabbitMQConn.CreateModel();
      

            for (int i = 0; i < 100; i++)
            {
                model.BasicPublish(
exchange: "amq.fanout",
routingKey: "other", // 不匹配 BindingKey
body: Encoding.UTF8.GetBytes($"我是通过应用发送的{i}条消息")
);

            }

            return true;
        }
    }
}
