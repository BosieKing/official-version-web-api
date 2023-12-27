using System.ComponentModel;

namespace SharedLibrary.Enums
{
    /// <summary>
    /// 菜单权重枚举
    /// </summary>
    public enum MenuWeightTypeEnum
    {
        /// <summary>
        /// 业务菜单
        /// </summary>
        [Description("业务菜单")]
        Service = 1,

        /// <summary>
        /// 系统运维级别菜单
        /// </summary>
        [Description("系统运维级别菜单")]
        SystemManage = 2,

        /// <summary>
        /// 租户定制化菜单
        /// </summary>
        [Description("租户定制化菜单")]
        Customization = 3,
    }
}
