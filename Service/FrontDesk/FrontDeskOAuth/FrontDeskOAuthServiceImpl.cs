using IDataSphere.Interfaces.FronDesk;
using Mapster;
using Model.Commons.CoreData;
using Model.DTOs.FronDesk.FrontDeskOAuth;
using Model.Repositotys.Service;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;
using UtilityToolkit.Tools;
using UtilityToolkit.Utils;
namespace Service.FrontDesk.FrontDeskOAuth
{
    /// <summary>
    /// 前台权限业务实现类
    /// </summary>
    public class FrontDeskOAuthServiceImpl : IFrontDeskOAuthService
    {
        #region 参数和构造函数
        /// <summary>
        /// 数据库访问
        /// </summary>
        private readonly IFrontDeskOAuthDao _frontDeskOAuthDao;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrontDeskOAuthServiceImpl(IFrontDeskOAuthDao frontDeskOAuthDao)
        {
            _frontDeskOAuthDao = frontDeskOAuthDao;

        }
        #endregion

      
    }
}
