using System.ComponentModel;

namespace SharedLibrary.Enums
{
    /// <summary>
    /// 验证码类型
    /// </summary>
    public enum VerificationCodeTypeEnum
    {
        /// <summary>
        /// 注册
        /// </summary>
        [Description("注册")]
        Register = 1,

        /// <summary>
        /// 登录
        /// </summary>
        [Description("登录")]
        Login = 2,

        /// <summary>
        /// 忘记密码
        /// </summary>
        [Description("忘记密码")]
        ForgetPwd = 3,

        /// <summary>
        /// 个人中心修改密码
        /// </summary>
        [Description("个人中心修改密码")]
        UpdatePwd = 4,
    }
}
