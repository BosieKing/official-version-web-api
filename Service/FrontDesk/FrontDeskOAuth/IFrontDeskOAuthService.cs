using Model.DTOs.FronDesk.FrontDeskOAuth;

namespace Service.FrontDesk.FrontDeskOAuth
{
    /// <summary>
    /// 前台权限业务接口
    /// </summary>
    public interface IFrontDeskOAuthService
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> ForgotPassword(ForgotPasswordInput input);

        /// <summary>
        /// 密码登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<(string Token, string RefreshToken)> LoginByPassWord(string phone, bool isRemember);

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> LoginOut(long userId, string token);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<(string Token, string RefreshToken)> Register(RegisteredInput input);
    }
}