namespace SharedLibrary.SystemConfigurations
{
    /// <summary>
    /// 验证码相关配置
    /// </summary>
    public class CaptchaConfig
    {
        /// <summary>
        /// 验证码出错过期时间
        /// </summary>
        public int VerifCodeErrorCountExpirationTime { get; set; }

        /// <summary>
        /// 验证码最大出错次数
        /// </summary>
        public int VerifCodeErrorMaxCount { get; set; }

        /// <summary>
        /// 滑动验证码过期时间
        /// </summary>
        public int GraphicCaptchaExpirationTime { get; set; }

        /// <summary>
        /// 用户密码出错次数过期时间
        /// </summary>
        public int PasswordErrorCountExpirationTime { get; set; }

        /// <summary>
        /// 密码最大出错次数
        /// </summary>
        public int PasswordErrorMaxCount { get; set; }

    }
}
