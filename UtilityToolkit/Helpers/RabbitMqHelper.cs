using RabbitMQ.Client;

namespace UtilityToolkit.Helpers
{
    /// <summary>
    /// RabbitMq帮助类
    /// </summary>
    public class RabbitMQHelper
    {
        private readonly IConnection connection;
        public RabbitMQHelper(IConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        public IConnection GetConnection()
        {
            ConnectionFactory conn = new ConnectionFactory();
            conn.HostName = "localhost";
            conn.VirtualHost = "/";
            conn.UserName = "guest";
            conn.Password = "guest";
            conn.Port = 5672;
            conn.AutomaticRecoveryEnabled = true;
            conn.NetworkRecoveryInterval = TimeSpan.FromSeconds(1);
            conn.TopologyRecoveryEnabled = true;
            return conn.CreateConnection();
        }


    }
}
