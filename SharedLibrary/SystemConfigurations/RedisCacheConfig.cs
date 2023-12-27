namespace SharedLibrary.SystemConfigurations
{
    /// <summary>
    /// Redis缓存配置
    /// </summary>
    public class RedisCacheConfig
    {
        /// <summary>
        /// 缓存类型
        /// </summary>
        public string CacheType { get; set; } = string.Empty;

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; } = string.Empty;

        /// <summary>
        /// 基础数据缓存连接字符串
        /// </summary>
        public string BaseDateCacheConnection { get; set; } = string.Empty;

        /// <summary>
        /// 用户信息缓存连接字符串
        /// </summary>
        public string UserCacheConnection { get; set; } = string.Empty;

        /// <summary>
        /// 验证码缓存连接字符串
        /// </summary>
        public string VerifyCacheConnection { get; set; } = string.Empty;

        /// <summary>
        /// 分布式锁连接字符串
        /// </summary>
        public string DistributeLockConnection { get; set; } = string.Empty;
    }
}
