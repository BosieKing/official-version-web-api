using SharedLibrary.SystemConfigurations;

namespace UtilityToolkit.Tools
{
    /// <summary>
    /// 配置文件内容访问工具类
    /// </summary>
    /// <remarks>热配置和冷配置使用此类访问</remarks>
    public static class ConfigSettingTool
    {
        /// <summary>
        /// JWT规范配置
        /// </summary>
        public static readonly JwtConfig JwtConfigOptions = new JwtConfig();

        /// <summary>
        /// 数据库连接配置
        /// </summary>
        public static readonly DBConnectionConfig ConnectionConfigOptions = new DBConnectionConfig();

        /// <summary>
        /// Redis连接配置
        /// </summary>
        public static readonly RedisCacheConfig RedisCacheConfigOptions = new RedisCacheConfig();

        /// <summary>
        /// 腾讯云短信配置
        /// </summary>
        public static readonly TencentSmsConfig TencentSmsConfigOptions = new TencentSmsConfig();

        /// <summary>
        /// 验证码配置
        /// </summary>
        public static readonly CaptchaConfig CaptchaConfigOptions = new CaptchaConfig();

        /// <summary>
        /// ES配置
        /// </summary>
        public static readonly ElasticSearchConfig ElasticSearchConfig = new ElasticSearchConfig();

        /// <summary>
        /// 系统配置
        /// </summary>

        public static readonly SystemConfig SystemConfig = new SystemConfig();
    }
}
