using System.ComponentModel;

namespace SharedLibrary.Enums
{
    
    /// <summary>
    /// 舞蹈类型枚举
    /// </summary>
    public enum DanceTypeEnum
    {
        /// <summary>
        /// 爵士舞
        /// </summary>
        [Description("爵士舞")]
        Jazz = 1,

        /// <summary>
        /// 恰恰舞
        /// </summary>
        [Description("恰恰舞")]
        ChaCha = 2,

        /// <summary>
        /// 拉丁舞
        /// </summary>
        [Description("拉丁舞")]
        Latin = 3,

        /// <summary>
        /// 中国舞
        /// </summary>
        [Description("中国舞")]
        ChineseDance = 4,

        /// <summary>
        /// 芭蕾舞
        /// </summary>
        [Description("芭蕾舞")]
        Ballet = 5,

        /// <summary>
        /// 街舞（Hip-hop）
        /// </summary>
        [Description("街舞")]
        Hiphop = 6,

        /// <summary>
        /// 探戈舞
        /// </summary>
        [Description("探戈")]
        Tango = 7,

        /// <summary>
        /// 莎莎舞
        /// </summary>
        [Description("莎莎舞")]
        Salsa = 8,

        /// <summary>
        /// 现代舞
        /// </summary>
        [Description("现代舞")]
        Contemporary = 9,

        /// <summary>
        /// Kpop
        /// </summary>
        [Description("Kpop")]
        BellyDance = 10
    }
}
