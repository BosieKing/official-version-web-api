using IDataSphere.DatabaseContexts;
using IDataSphere.Interfaces.BackEnd;
using Model.Repositotys.Log;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 报错日志
    /// </summary>
    public class ErrorLogDao : BaseDao<TL_ErrorLog>, IErrorLogDao
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlDbContext"></param>
        public ErrorLogDao(SqlDbContext sqlDbContext) : base(sqlDbContext)
        {
        }
    }
}
