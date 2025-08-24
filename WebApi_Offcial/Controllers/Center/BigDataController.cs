using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using SharedLibrary.Enums;

namespace WebApi_Offcial.Controllers.Center
{
    /// <summary>
    /// 测试专用控制器
    /// </summary>
    [Route("BigData")]
    [ApiController]
    [ApiDescription(SwaggerGroupEnum.Center)]
    [AllowAnonymous]
    public class BigDataController : ControllerBase
    {
        private readonly IConnection _rabbitMQConn;
        public BigDataController(IConnection rabbitMQConn)
        {
            this._rabbitMQConn = rabbitMQConn;
        }
        
    }
}
