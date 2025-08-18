using IDataSphere.Interfaces.FronDesk;
using Quartz;

namespace WebApi_Offcial.Quartz
{
    /// <summary>
    /// 自动确认课程完成任务
    /// </summary>
    [DisallowConcurrentExecution]
    public class FinshedCourseJob : IJob
    {
        private readonly IHomeDao _homeDao;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorLogDao"></param>
        public FinshedCourseJob(IHomeDao homeDao)
        {
            _homeDao = homeDao;
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
           await  _homeDao.FinshCourseSignUp();
            await _homeDao.FinshPersonalCourseSignUp();
        }
    }
}
