using IDataSphere.Interfaces.BackEnd;
using Mapster;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.UserManage;
using Model.Repositotys.BasicData;
using Model.Repositotys.Service;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;

namespace Service.BackEnd.UserManage
{
    /// <summary>
    /// 后台用户管理业务实现类
    /// </summary>
    public class UserManageServiceImpl : IUserManageService
    {
        #region 构造函数
        private readonly IUserManageDao _userManageDao;
        public UserManageServiceImpl(IUserManageDao userManageDao)
        {
            _userManageDao = userManageDao;
        }
        #endregion


    }
}
