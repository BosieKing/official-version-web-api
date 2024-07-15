using System.ComponentModel;

namespace SharedLibrary.Enums
{
    /// <summary>
    /// Redis缓存类型枚举
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

        /// <summary>
        /// 帖子模块相关数据缓存
        /// </summary>
        [Description("帖子模块相关数据缓存")]
        PostCache = 5,
    }
}
