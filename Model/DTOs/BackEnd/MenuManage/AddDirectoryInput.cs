using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.MenuManage
{
    /// <summary>
    /// 新增目录输入类
    /// </summary>
    public class AddDirectoryInput
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
        ///  前端浏览器路由地址--前端维护
        /// </summary>
        [Required(ErrorMessage = "PathRequired")]
        [MaxLength(100, ErrorMessage = "PathTooLong100")]
        public string BrowserPath { get; set; }

    }
}
