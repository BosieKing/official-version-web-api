using Quartz;
using Quartz.Spi;
using WebApi_Offcial.Quartz;

namespace WebApi_Offcial.ConfigureServices
{
    /// <summary>
    /// 应用启动和关闭事件
    /// </summary>
    public class QuartzHostService : IHostedService
    {
        private readonly IJobFactory _jobFactory;
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;
        private IEnumerable<JobDescription> _jobDescriptions;

        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="jobFactory">任务工厂</param>
        /// <param name="schedulerFactory">调度器工厂</param>
        public QuartzHostService(IJobFactory jobFactory, ISchedulerFactory schedulerFactory, IEnumerable<JobDescription> jobDescriptions)
        {
            this._jobFactory = jobFactory;
            this._schedulerFactory = schedulerFactory;
            this._jobDescriptions = jobDescriptions;
        }

        /// <summary>
        /// 启动事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // 通过工厂得到一个调度器      
            this._scheduler = await _schedulerFactory.GetScheduler(cancellationToken);// 传入token以便在程序停止的时候释放资源
            // 告诉调度器创建任务使用JobFactory的实现，而我们自己定义的实现是从依赖注入容器中获取
            this._scheduler.JobFactory = this._jobFactory;
            foreach (var item in this._jobDescriptions)
            {
                var job = CreateJobDetail(item);
                var trigger = CreateTrigger(item);
                // 在调度器中添加任务
                await this._scheduler.ScheduleJob(job, trigger, cancellationToken);
            }
            // 启动定时任务
            try
            {
                await this._scheduler.Start(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            // 释放资源
            return this._scheduler?.Shutdown(cancellationToken) ?? Task.CompletedTask;
        }

        /// <summary>
        /// 得到job的实例描述
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        private IJobDetail CreateJobDetail(JobDescription description)
        {
            return JobBuilder
                .Create(description.JobType)
                .WithIdentity(description.Id) // 设置唯一标识符
                .Build();
        }

        /// <summary>
        /// 得到调度器
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        private ITrigger CreateTrigger(JobDescription description)
        {
            return TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule(description.CronExpression)
                .WithIdentity(description.TriggerId)
                .Build();
        }

    }
}
