namespace WebApi_Offcial.Quartz
{
    /// <summary>
    /// 自定义任务描述
    /// </summary>
    public class JobDescription
    {
        /// <summary>
        /// 初始化描述
        /// </summary>
        /// <param name="jobType"></param>
        /// <param name="cronExpression"></param>
        public JobDescription(Type jobType, string cronExpression)
        {
            this.JobType = jobType;
            this.CronExpression = cronExpression;
            this.Id = jobType.FullName;
            this.TriggerId = jobType.FullName + "TriggerID";
        }

        /// <summary>
        /// 唯一id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// 该任务配套的调度器的id
        /// </summary>
        public string TriggerId { get; private set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public Type JobType { get; private set; }

        /// <summary>
        /// cron表达式
        /// </summary>
        public string CronExpression { get; private set; }
    }


}
