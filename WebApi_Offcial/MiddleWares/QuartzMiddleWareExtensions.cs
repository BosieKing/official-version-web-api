using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using WebApi_Offcial.ConfigureServices;
using WebApi_Offcial.Quartz;

namespace WebApi_Offcial.MiddleWares
{
    /// <summary>
    /// 自定义Quartz服务中间件拓展
    /// </summary>
    public static class QuartzMiddleWareExtensions
    {
        /// <summary>
        /// 注入Quartz服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomizeQuartz(this IServiceCollection services)
        {
            // 注入服务支持    
            services.AddQuartz();
            // 注入主机事件
            services.AddHostedService<QuartzHostService>();
            // 注入job服务
            services.AddJobs();
            // 采用单例模式实现。  
            services.AddSingleton<IJobFactory, JobFactory>();
            // StdSchedulerFactory已经由quartz内部实现，无需手动实现ISchedulerFactory
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();// 用于实现读取配置文件，初始化调度器线程池、作业存储、触发器存储等工作。
            return services;
        }

        /// <summary>
        /// 注入需要执行的任务列表
        /// </summary>
        /// <returns></returns>
        private static void AddJobs(this IServiceCollection services)
        {
            // 配套实现，如注入一个服务，则需要添加一个描述类
            // 不通过接口注入，不然在JobFactory中无法找到对应的实例
            services.AddSingleton<WirteLogJob>();
            services.AddSingleton(new JobDescription(jobType: typeof(WirteLogJob), cronExpression: "*/5 * * * * ?"));
        }
    }
}
