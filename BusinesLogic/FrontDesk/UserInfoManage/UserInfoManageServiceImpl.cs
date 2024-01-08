using IDataSphere.Interfaces.FronDesk;
using Mapster;
using Model.DTOs.FronDesk.UserInfoManage;
using Model.Repositotys;
using UtilityToolkit.Helpers;

namespace BusinesLogic.FrontDesk.UserInfoManage
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
        public async Task<dynamic> GetUserInfo(long userId)
        {
            var userInfo = await _userInfoDao.GetUserInfoById(userId);
            var bindTenantList = await _userInfoDao.GetTenantBindList(userId);
            return new
            {
                userInfo,
                bindTenantList
            };
        }
        #endregion

        #region 更新
        /// <summary>
        /// 完善个人信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserInfo(CompleteUserInfoInput input, long userId)
        {
            T_User user = input.Adapt<T_User>();
            user.Id = userId;
            return await _userInfoDao.UpdateUserInfo(user);
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UploadAvatar(string url, long userId)
        {
            return await _userInfoDao.UpdateAvatar(url, userId);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassword(string password, long userId, string token)
        {
            await RedisMulititionHelper.LoginOut(userId.ToString(), token);
            return await _userInfoDao.UpdatePassword(password, userId);
        }
        #endregion

        #region 删除
        #endregion
    }
}
