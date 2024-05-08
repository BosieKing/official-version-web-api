using RabbitMQ.Client;

namespace UtilityToolkit.Helpers
{
    /// <summary>
    /// RabbitMq帮助类
    /// </summary>
    public class RabbitMqHelper
    {
        public void Test()
        {
            // 创建连接池工厂
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "liujie",
                Password = "liujie123",
                Port = 5672,
                AutomaticRecoveryEnabled = true,
            };
            // 通过工厂创建连接
            var connection = connectionFactory.CreateConnection();
            // 得到管道
            var channel = connection.CreateModel();
            channel.QueueDeclare(
                queue: "ErrorLog"
            );
        }
    }
}
