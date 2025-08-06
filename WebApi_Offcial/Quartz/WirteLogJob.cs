using Quartz;

namespace WebApi_Offcial.Quartz
{
    /// <summary>
    /// 写入日志
    /// </summary>
    [DisallowConcurrentExecution]
    public class WirteLogJob : IJob
    {
     
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorLogDao"></param>
        public WirteLogJob()
        {
           
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
