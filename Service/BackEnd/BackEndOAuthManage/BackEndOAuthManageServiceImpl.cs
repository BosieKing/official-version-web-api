using IDataSphere.Interfaces.BackEnd;
using Model.Commons.CoreData;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.BackEndOAuthManage;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;
using UtilityToolkit.Tools;
using UtilityToolkit.Utils;

namespace Service.BackEnd.BackEndOAuthManage
{
    /// <summary>
    /// 后台权限管理业务实现类
    /// </summary>
    public class BackEndOAuthManageServiceImpl : IBackEndOAuthManageService
    {
        #region 构造函数
        /// <summary>
        /// 数据库访问
        /// </summary>
        private readonly IBackEndOAuthDao _backEndOAuthDao;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BackEndOAuthManageServiceImpl(IBackEndOAuthDao backEndOAuthDao)
        {
            _backEndOAuthDao = backEndOAuthDao;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 获取左侧菜单权限树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<MenuTreeModel>> GetMenuTree(string rolds)
        {
            long[] ids = rolds.Split(",").Select(p => long.Parse(p)).ToArray();
            return await _backEndOAuthDao.GetMenuTree(ids);
        }

        /// <summary>
        /// 获取左侧菜单权限树
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuTreeModel>> GetSuperManageMenuTree()
        {
            return await _backEndOAuthDao.GetSuperManageMenuTree();
        }

        /// <summary>
        /// 获取按钮集合
        /// </summary>
        /// <param name="rolds"></param>
        /// <returns></returns>
        public async Task<string[]> GetButtonArray(string rolds)
        {
            var ids = rolds.Split(",").Select(p => long.Parse(p)).ToArray();
            return await _backEndOAuthDao.GetButtonArray(ids);
        }

        /// <summary>
        /// 获取超管按钮集合
        /// </summary>
        /// <param name="rolds"></param>
        /// <returns></returns>
        public async Task<string[]> GetSuperManageButtonArray()
        {
            return await _backEndOAuthDao.GetSuperManageButtonArray();
        }

        /// <summary>
        /// 获取个人中心信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> GetUserInfo(long userId)
        {
            return await _backEndOAuthDao.GetUserInfoById(userId);
        }

        /// <summary>
        /// 获取已绑定的租户id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DropdownDataResult>> GetBindTenantList(long userId)
        {
            return await _backEndOAuthDao.GetBindTenantList();
        }

        /// <summary>
        /// 超管获取已绑定的租户id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DropdownDataResult>> GetSuperManageBindTenantList(long userId)
        {
            return await _backEndOAuthDao.GetSuperManageBindTenantList(userId);
        }
        #endregion

        #region 登录
        /// <summary>
        /// 密码登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<(string Token, string RefreshToken)> LoginByPassWord(bool isRemember, string phone = "", long userId = 0, long tenantId = 0)
        {
            // 判断电话号码是否为超管
            var isSuperManage = await _backEndOAuthDao.IsSuperManage(phone: phone);
            TokenInfoModel user;
            if (isSuperManage)
                user = await _backEndOAuthDao.GetSuperManageUserInfoByPhone(phone: phone);
            else
                user = await _backEndOAuthDao.GetUserInfoByPhone(phone: phone);
            var token = TokenTool.CreateToken(user, RedisMulititionHelper.IsSuperManage(user.TenantId));
            var refreshToken = TokenTool.CreateRefreshToken(user, isRemember);
            // 缓存数据
            var key = UserCacheConst.USER_INFO_TABLE + user.TenantId;
            var value = user.ToJson();
            await RedisMulititionHelper.GetClinet(CacheTypeEnum.User).HMSetAsync(key, user.UserId.ToString(), value);
            await RedisMulititionHelper.GetClinet(CacheTypeEnum.User).HDelAsync(UserCacheConst.MAKE_IN_TABLE, userId.ToString());
            return new ValueTuple<string, string>(token, refreshToken);
        }

        /// <summary>
        /// 切换平台
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tenantId"></param>
        /// <param name="isRemember"></param>
        /// <returns></returns>
        public async Task<(string Token, string RefreshToken)> SwitchTenant(long userId, long tenantId, bool isRemember)
        {
            // 判断电话号码是否为超管
            var isSuperManage = await _backEndOAuthDao.IsSuperManage(userId: userId);
            TokenInfoModel user;
            if (isSuperManage)
                user = await _backEndOAuthDao.GetSuperManageUserInfoByPhone(userId: userId, tenantId: tenantId);
            else
                user = await _backEndOAuthDao.GetUserInfoByPhone(userId: userId, tenantId: tenantId);
            var token = TokenTool.CreateToken(user, RedisMulititionHelper.IsSuperManage(user.TenantId));
            var refreshToken = TokenTool.CreateRefreshToken(user, isRemember);
            // 缓存数据
            var key = UserCacheConst.USER_INFO_TABLE + user.TenantId;
            var value = user.ToJson();
            await RedisMulititionHelper.GetClinet(CacheTypeEnum.User).HDelAsync(UserCacheConst.MAKE_IN_TABLE, userId.ToString());
            await RedisMulititionHelper.GetClinet(CacheTypeEnum.User).HMSetAsync(key, user.UserId.ToString(), value);
            return new ValueTuple<string, string>(token, refreshToken);
        }

        #endregion

        #region 新增

        #endregion

        #region 修改
        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UploadAvatar(string url, long userId)
        {
            return await _backEndOAuthDao.UpdateAvatar(url, userId);
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserInfo(UpdateUserInfoInput input, long userId)
        {
            return await _backEndOAuthDao.UpdateUserInfo(input.NickName, input.Email, userId);
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassword(BackEndUpdatePasswordInput input, long userId)
        {
            return await _backEndOAuthDao.UpdatePassword(input.NewPassword, userId);
        }
        #endregion

    }
}
