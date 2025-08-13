using RabbitMQ.Client;

namespace UtilityToolkit.Helpers
{
    /// <summary>
    /// RabbitMq帮助类
    /// </summary>
    public class RabbitMQHelper
    {
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        public IConnection GetConnection()
        {
            var conn = new ConnectionFactory();
            conn.HostName = "localhost";
            conn.VirtualHost = "/";
            conn.UserName = "guest";
            conn.Password = "guest";
            conn.Port = 5672;
            return conn.CreateConnection();
        }

     

    }
}
