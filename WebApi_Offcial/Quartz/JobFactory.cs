using Quartz;
using Quartz.Spi;

namespace WebApi_Offcial.Quartz
{
    /// <summary>
    /// 任务工厂
    /// </summary>
    public class JobFactory : IJobFactory
    {
        /// <summary>
        /// 注入依赖容器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 获取Job
        /// </summary>
        /// <param name="bundle">封装在触发器触发的时候，需要传递给job的相关信息。每当触发器触发的时候，都会创造一个该实例</param>
        /// <param name="scheduler">调度器</param>
        /// <returns></returns> 
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            // 从依赖注入容器获取注入的任务
            // JobDetail：描述job的类，包括job的名字、组名、描述等，是核心部分。通过JobBuilder创建
            return _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
        }

        /// <summary>
        /// 释放job实例
        /// </summary>
        /// <param name="job"></param>
        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();
            // 每当一个job执行完成后，都需要通过这个方法来通知JobFactory释放掉该实例。
        }
    }
}

