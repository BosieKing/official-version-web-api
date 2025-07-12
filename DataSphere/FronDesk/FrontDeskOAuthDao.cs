using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.FronDesk;
using Microsoft.EntityFrameworkCore;
using Model.Commons.CoreData;
using Model.Repositotys.Service;

namespace DataSphere.FronDesk
{
    /// <summary>
    /// 前台权限业务数据访问实现类
    /// </summary>
    public class FrontDeskOAuthDao : BaseDao<T_User>, IFrontDeskOAuthDao
    {
        #region 构造函数
        public FrontDeskOAuthDao(SqlDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

    }
}
