using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.BackEnd;
using Microsoft.EntityFrameworkCore;
using Model.Commons.CoreData;
using Model.Commons.Domain;
using Model.Repositotys.Service;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;
using UtilityToolkit.Utils;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 后台权限管理数据访问实现类
    /// </summary>
    public class BackEndOAuthDao : BaseDao<T_User>, IBackEndOAuthDao
    {
        #region 构造函数
        public BackEndOAuthDao(SqlDbContext dbContext) : base(dbContext)
        {

        }
        #endregion

    }
}
