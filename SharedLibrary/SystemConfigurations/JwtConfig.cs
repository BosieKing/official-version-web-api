namespace SharedLibrary.SystemConfigurations
{
    /// <summary>
    /// JWT配置文件
    /// </summary>
    public class JwtConfig
    {
        /// <summary>
        /// 是否验证密钥
        /// </summary>
        public bool IsValidateIssuerSigningKey { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string IssuerSigningKey { get; set; } = string.Empty;

        /// <summary>
        /// 是否验证签发方
        /// </summary>
        public bool IsValidateIssuer { get; set; }

        /// <summary>
        /// 签发方
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// 是否验证签收方
        /// </summary>
        public bool IsValidateAudience { get; set; }

        /// <summary>
        /// 签收方
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// 是否验证过期时间
        /// </summary>
        public bool IsValidateExpirationTime { get; set; }

        /// <summary>
        /// 普通token过期时间
        /// </summary>
        public int ExpirationTime { get; set; }

        /// <summary>
        /// 刷新token过期时间
        /// </summary>
        public int RefreshTokenExpirationTime { get; set; }

        /// <summary>
        /// 是否验证token被强制注销
        /// </summary>
        public bool CheckSignoutToken { get; set; }
    }
}
