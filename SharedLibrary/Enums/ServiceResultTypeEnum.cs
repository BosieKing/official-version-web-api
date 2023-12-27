using System.ComponentModel;

namespace SharedLibrary.Enums
{
    /// <summary>
    /// 返回类型枚举
    /// </summary>
    public enum ServiceResultTypeEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Succeed = 200,
        /// <summary>
        /// 一般性失败
        /// </summary>
        [Description("一般失败")]
        Fail = 500,
        /// <summary>
        /// 参数错误
        /// </summary>
        [Description("参数错误")]
        Error = 400,
    }
}
