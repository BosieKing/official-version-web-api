using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.MenuManage
{
    /// <summary>
    /// 新增菜单输入类
    /// </summary>
    public class AddMenuInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        [MaxLength(50, ErrorMessage = "NameTooLong50")]
        public string Name { get; set; }

        /// <summary>
        /// 图标--前端需要使用字段
        /// </summary>       
        [Required(ErrorMessage = "IconRequired")]
        [MaxLength(50, ErrorMessage = "IconTooLong50")]
        public string Icon { get; set; }

        /// <summary>
        /// 后端权限路由-对应的控制器名称
        /// </summary>
        [Required(ErrorMessage = "RouterRequired")]
        [MaxLength(100, ErrorMessage = "RouterTooLong100")]
        public string ControllerRouter { get; set; }

        /// <summary>
        ///  前端浏览器路由地址--前端维护
        /// </summary>
        [Required(ErrorMessage = "PathRequired")]
        [MaxLength(100, ErrorMessage = "PathTooLong100")]
        public string BrowserPath { get; set; }

        /// <summary>
        /// 前台组件物理地址--前端维护
        /// </summary>
        [Required(ErrorMessage = "ComponentRequired")]
        [MaxLength(100, ErrorMessage = "ComponentTooLong100")]
        public string VueComponent { get; set; }

        /// <summary>
        /// 菜单权重枚举
        /// </summary>
        /// <see cref="SharedLibrary.Enums.MenuWeightTypeEnum"/>
        [Required(ErrorMessage = "TypeRequired")]
        public int Weight { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "RemarkTooLong500")]
        public string Remark { get; set; }

        /// <summary>
        /// 目录id
        /// </summary>
        [Required(ErrorMessage = "DirectoryIdRequired")]
        public long DirectoryId { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; } = false;
    }
}
