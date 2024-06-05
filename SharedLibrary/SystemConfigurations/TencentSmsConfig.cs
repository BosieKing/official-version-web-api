namespace SharedLibrary.SystemConfigurations
{
    /// <summary>
    /// 腾讯云短信配置
    /// </summary>
    public class TencentSmsConfig
    {
        /// <summary>
        /// 腾讯云SecretId
        /// </summary>
        public string SecretId { get; set; }

        /// <summary>
        /// 腾讯云SecretKey
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 腾讯云短信SDKId
        /// </summary>
        public string SmsSdkAppId { get; set; }

        /// <summary>
        /// 腾讯云签名
        /// </summary>
        public string SmsSign { get; set; }

        /// <summary>
        /// 过期时间，单位秒
        /// </summary>
        public int SmsExpirationTime { get; set; }

        /// <summary>
        /// 腾讯云短信模板Id
        /// </summary>
        public string SmsTmplId { get; set; }

    }
}
