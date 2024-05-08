using System.ComponentModel;

namespace SharedLibrary.Enums
{
    /// <summary>
    /// Swagger分组枚举
    /// </summary>
    public enum SwaggerGroupEnum
    {
        /// <summary>
        /// 后台接口
        /// </summary>
        [Description("业务后台接口")]
        BackEnd = 1,

        /// <summary>
        /// 前台接口
        /// </summary>
        [Description("业务前台接口")]
        FrontDesk = 2,

        /// <summary>
        /// 中间服务
        /// </summary>
        [Description("中间服务")]
        Center = 3,

        /// <summary>
        /// 运营管理接口
        /// </summary>
        [Description("运营管理接口")]
        System = 4,

        /// <summary>
        /// 塔罗
        /// </summary>
        [Description("塔罗")]
        Tarot = 5,
    }
}
