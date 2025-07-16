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
namespace Service.FrontDesk.Home
{
    /// <summary>
    /// 前台权限业务实现类
    /// </summary>
    public class HomeImpl : IHomeService
    {
        #region 参数和构造函数
        /// <summary>
        /// 数据库访问
        /// </summary>
        private readonly IHomeDao _homeDao;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HomeImpl(IHomeDao homeDao)
        {
            _homeDao = homeDao;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 获取导师列表
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetMentorList ()
        { 
        return _homeDao.


        }
        #endregion


    }
}
