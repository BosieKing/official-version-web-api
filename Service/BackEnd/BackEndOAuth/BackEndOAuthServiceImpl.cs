using IDataSphere.DatabaseContexts;
using IDataSphere.Interfaces.BackEnd;
using Model.Commons.CoreData;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.BackEndOAuth;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;
using UtilityToolkit.Tools;
using UtilityToolkit.Utils;

namespace Service.BackEnd.BackEndOAuth
{
    /// <summary>
    /// 后台权限管理业务实现类
    /// </summary>
    public class BackEndOAuthServiceImpl : IBackEndOAuthService
    {
        #region 构造函数
        /// <summary>
        /// 数据库访问
        /// </summary>
        private readonly IBackEndOAuthDao _backEndOAuthDao;
        private readonly UserProvider _user;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BackEndOAuthServiceImpl(IBackEndOAuthDao backEndOAuthDao, UserProvider user)
        {
            _backEndOAuthDao = backEndOAuthDao;
            _user = user;
        }
        #endregion
    }
}
