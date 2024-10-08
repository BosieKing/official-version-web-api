using System.ComponentModel;

namespace SharedLibrary.Enums
{
    /// <summary>
    /// 菜单目录树类型枚举
    /// </summary>
    public enum MenuTreeTypeEnum
    {
        /// <summary>
        /// 目录
        /// </summary>
        [Description("目录")]
        Directory = 1,

        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu = 2,

        /// <summary>
        /// 按钮
        /// </summary>
        [Description("按钮")]
        Button = 3,
    }
}
