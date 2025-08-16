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
        /// 死信队列
        /// </summary>
        /// <returns></returns>
        [HttpGet("DieQueueTest")]
        public ActionResult<dynamic> DieQueueTest()
        {
          
            using (var model = _rabbitMQConn.CreateModel())
            {
                // 声明一个死信队列
               
                model.ExchangeDeclare("die-exchange", ExchangeType.Direct, false, false);
                model.QueueDeclare("die-queue", false, false, false);
                // 不设置routingkey，目的是所有进入死信交换机的消息都会路由到，因为如果原来消息发送的指定的key会和死信息的不一致，导致入不了列
                model.QueueBind("die-queue", "die-exchange", "");

                // 声明一个正常的队列处理业务
                model.ExchangeDeclare("main-exchange", ExchangeType.Direct, false, false);
                // 内置参数，x-dead-letter-exchange表示当收到reject的时候转发到哪里
                Dictionary<string, object> arguments = new Dictionary<string, object>();
                arguments.Add("x-dead-letter-exchange", "die-exchange");
                model.QueueDeclare("main-queue", false,false,false);
                model.QueueBind("main-queue", "main-exchange", "main");

                for (int i = 0; i < 3; i++)
                {
                    var property = model.CreateBasicProperties();
                    property.MessageId = "1";
                    property.Persistent = true;
                    model.BasicPublish("main-exchange", "main", body: Encoding.UTF8.GetBytes($"你好呀{i}号"));
                }
            }
  
            return true;
        }

        /// <summary>
        /// 设置消息的唯一id
        /// </summary>
        /// <returns></returns>
        [HttpGet("SetMessgaeId")]
        public ActionResult<dynamic> SetMessgaeId()
        {
            using (var model = _rabbitMQConn.CreateModel())
            {
                model.ExchangeDeclare("main-exchange", ExchangeType.Direct, false, false);
                model.QueueDeclare("main-queue", false, false, false);
                model.QueueBind("main-queue", "main-exchange", "test");

                var properties = model.CreateBasicProperties();
                properties.MessageId = "2";
                properties.Persistent = true;

                for (int i = 0; i < 2; i++)
                {
                    model.BasicPublish("main-exchange", "test", false, properties, Encoding.UTF8.GetBytes($"你好呀{i}"));
                }
            }
            return true;
        }

        /// <summary>
        /// 延迟队列
        /// </summary>
        /// <returns></returns>
        [HttpGet("SetDelayQueue")]
        public ActionResult<dynamic> SetDelayQueue()
        {
            using (var model = _rabbitMQConn.CreateModel())
            {
                model.QueueDelete("pay-die-queue");
                model.QueueDelete("pay-queue");
                model.ExchangeDelete("pay-exchange");
                model.ExchangeDelete("pay-die-exchange");

                // 设置一个处理支付30s内未支付的死信交换机
                model.ExchangeDeclare("pay-die-exchange", ExchangeType.Direct, false, false);
                model.QueueDeclare("pay-die-queue", false, false, false);
                model.QueueBind("pay-die-queue", "pay-die-exchange", "dead");



                // 设置一个下单的支付的交换机
                model.ExchangeDeclare("pay-exchange", ExchangeType.Direct, false, false);
                // 设置这个队列的死信是进入哪个死信队列
                Dictionary<string, object> arguments = new Dictionary<string, object>();
                arguments.Add("x-dead-letter-exchange", "pay-die-exchange");
                arguments.Add("x-dead-letter-routing-key", "dead");
                model.QueueDeclare("pay-queue", false, false, false, arguments);
                model.QueueBind("pay-queue", "pay-exchange", "pay");

               


                var properties = model.CreateBasicProperties();
                properties.Expiration = "8000";
                for (int i = 0; i < 2; i++)
                {
                    model.BasicPublish("pay-exchange", "pay", false, properties, Encoding.UTF8.GetBytes($"你好呀{i}"));
                }
            }
            return true;
        }
    }
}
