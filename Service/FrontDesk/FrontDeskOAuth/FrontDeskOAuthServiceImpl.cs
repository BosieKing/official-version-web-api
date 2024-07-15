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

        #region 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<(string Token, string RefreshToken)> Register(RegisteredInput input)
        {
            T_User user = input.Adapt<T_User>();
            (long Id, string Code) tenantInfo = await _frontDeskOAuthDao.GetIdByInviteCode(input.InviteCode);
            user.TenantId = tenantInfo.Id;
            await _frontDeskOAuthDao.AddUser(user);
            var tokenResult = new TokenInfoModel
            {
                UserId = user.Id.ToString(),
                TenantId = user.TenantId.ToString(),
                RoleIds = "",
                SchemeName = tenantInfo.Code,
            };
            bool isSuperManage = RedisMulititionHelper.IsSuperManage(tokenResult.TenantId);
            string token = TokenTool.CreateToken(tokenResult, isSuperManage);
            string refreshToken = TokenTool.CreateRefreshToken(tokenResult, false);
            return new ValueTuple<string, string>(token, refreshToken);
        }
        #endregion

        #region 登录
        /// <summary>
        /// 登录颁发token
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<(string Token, string RefreshToken)> LoginByPassWord(string phone, bool isRemember)
        {
            TokenInfoModel user = await _frontDeskOAuthDao.GetUserInfoByPhone(phone);
            string token = TokenTool.CreateToken(user, RedisMulititionHelper.IsSuperManage(user.TenantId));
            string refreshToken = TokenTool.CreateRefreshToken(user, isRemember);
            // 缓存数据
            string key = UserCacheConst.USER_INFO_TABLE + user.TenantId;
            string value = user.ToJson();
            await _frontDeskOAuthDao.UpdateLastLoginTime(long.Parse(user.UserId));
            await RedisMulititionHelper.GetClient(CacheTypeEnum.User).HMSetAsync(key, user.UserId.ToString(), value);
            await RedisMulititionHelper.GetClient(CacheTypeEnum.User).HDelAsync(UserCacheConst.MAKE_IN_TABLE, user.UserId.ToString());
            return new ValueTuple<string, string>(token, refreshToken);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="refreshToken"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> LoginOut(long userId, string token)
        {
            return await RedisMulititionHelper.LoginOut(userId.ToString(), token);
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> ForgotPassword(ForgotPasswordInput input)
        {
            return await _frontDeskOAuthDao.UpdateLastLoginPassWord(input.Phone, input.NewPassWord);
        }
        #endregion
    }
}
