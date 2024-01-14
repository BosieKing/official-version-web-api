namespace WebApi_Offcial.ConfigureServices
{
    /// <summary>
    /// 应用启动和关闭事件
    /// </summary>
    public class ApplicationStartService : IHostedService
    {
        /// <summary>
        /// 启动事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
