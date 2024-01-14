using IDataSphere.Interfaces.FronDesk;
using Mapster;
using Model.Commons.Domain;
using Model.DTOs.FronDesk.UserInfoManage;
using Model.Repositotys;
using UtilityToolkit.Helpers;

namespace Service.FrontDesk.UserInfoManage
{
    /// <summary>
    /// 前台用户中心实现类
    /// </summary>
    public class UserInfoManageServiceImpl : IUserInfoManageService
    {
        #region 构造函数
        private readonly IUserInfoManageDao _userInfoDao;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserInfoManageServiceImpl(IUserInfoManageDao userInfoDao)
        {
            _userInfoDao = userInfoDao;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 根据用户id查询用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetUserInfo()
        {
            long userId = _userInfoDao.UserId();
            return await _userInfoDao.GetUserInfoById(userId);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 完善个人信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserInfo(CompleteUserInfoInput input)
        {
            T_User user = input.Adapt<T_User>();
            user.Id = _userInfoDao.UserId();
            return await _userInfoDao.UpdateUserInfo(user);
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UploadAvatar(string url)
        {
            return await _userInfoDao.UpdateAvatar(url, _userInfoDao.UserId());
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassword(string password, string token)
        {
            long userId = _userInfoDao.UserId();
            await RedisMulititionHelper.LoginOut(userId.ToString(), token);
            return await _userInfoDao.UpdatePassword(password, userId);
        }
        #endregion


    }
}
