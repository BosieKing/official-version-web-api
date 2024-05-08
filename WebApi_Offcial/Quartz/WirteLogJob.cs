using IDataSphere.Interfaces.BackEnd;
using Model.Repositotys.Log;
using Quartz;
using UtilityToolkit.Helpers;

namespace WebApi_Offcial.Quartz
{
    /// <summary>
    /// 写入日志
    /// </summary>
    [DisallowConcurrentExecution]
    public class WirteLogJob : IJob
    {
        private readonly IErrorLogDao _errorLogDao;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorLogDao"></param>
        public WirteLogJob(IErrorLogDao errorLogDao)
        {
            _errorLogDao = errorLogDao;
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            var list = QueueSingletonHelper<TL_ErrorLog>.Instance.GetQueue(5);
            _errorLogDao.BatchAddAsync(list);
            return Task.CompletedTask;
        }
    }
}
