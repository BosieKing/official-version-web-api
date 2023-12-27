using BusinesLogic.FrontDesk.FrontDeskOAuth.Dto;

namespace BusinesLogic.FrontDesk.FrontDeskOAuth
{
    /// <summary>
    /// 前台权限操作接口
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
        Task<bool> LoginOut(long userId, string token);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<(string Token, string RefreshToken)> Register(RegisteredInput input);
    }
}