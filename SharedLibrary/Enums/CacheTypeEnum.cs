using System.ComponentModel;

namespace SharedLibrary.Enums
{
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheTypeEnum
    {
        /// <summary>
        /// 基础数据
        /// </summary>
        [Description("基础数据")]
        BaseData = 1,

        /// <summary>
        /// 用户信息
        /// </summary>
        [Description("用户信息")]
        User = 2,

        /// <summary>
        /// 验证信息
        /// </summary>
        [Description("验证信息")]
        Verify = 3,

        /// <summary>
        /// 分布式锁
        /// </summary>
        [Description("分布式锁")]
        Distributed = 4,
    }
}
