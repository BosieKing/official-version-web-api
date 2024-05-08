using System.ComponentModel;

namespace SharedLibrary.Enums
{
    /// <summary>
    /// 表分类
    /// </summary>
    public enum TableGroupEnum
    {
        /// <summary>
        /// 基础数据
        /// </summary>
        [Description("基础数据")]
        BasicData = 1,

        /// <summary>
        /// 日志
        /// </summary>
        [Description("日志")]
        Log = 2,

        /// <summary>
        /// 中间服务
        /// </summary>
        [Description("业务服务")]
        Service = 3,

        /// <summary>
        /// Tarot
        /// </summary>
        [Description("Tarot")]
        Tarot = 4,
    }
}
