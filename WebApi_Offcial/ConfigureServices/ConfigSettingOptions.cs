using SharedLibrary.SystemConfigurations;
using UtilityToolkit.Tools;

namespace WebApi_Offcial.ConfigureServices
{
    /// <summary>
    /// 配置注入
    /// </summary>
    public static class ConfigSettingOptions
    {
        /// <summary>
        /// 将配置注入的依赖容器中
        /// </summary>
        /// <param name="builder"></param>
        /// <remarks>适用于需要动态更改的配置</remarks>
        /// <returns></returns>
        public static WebApplicationBuilder AddConfigSettingRegister(this WebApplicationBuilder builder)
        {
            // 冷配置
            // builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(nameof(JwtConfig)));

            // 热配置
            return builder;
        }

        /// <summary>
        /// 将配置与模型绑定
        /// </summary>
        /// <param name="configuration"></param>
        /// <remarks>适用于为不轻易更改的配置</remarks>
        /// <returns></returns>
        public static IConfiguration AddConfigSettingBind(this IConfiguration configuration)
        {
            // 冷配置
            configuration.GetSection(nameof(JwtConfig)).Bind(ConfigSettingTool.JwtConfigOptions);
            configuration.GetSection(nameof(DBConnectionConfig)).Bind(ConfigSettingTool.ConnectionConfigOptions);
            configuration.GetSection(nameof(RedisCacheConfig)).Bind(ConfigSettingTool.RedisCacheConfigOptions);
            configuration.GetSection(nameof(SmsConfig)).Bind(ConfigSettingTool.SmsConfigOptions);
            configuration.GetSection(nameof(CaptchaConfig)).Bind(ConfigSettingTool.CaptchaConfigOptions);
            configuration.GetSection(nameof(ElasticSearchConfig)).Bind(ConfigSettingTool.ElasticSearchConfig);
            return configuration;
        }
    }
}
