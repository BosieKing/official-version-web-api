using System.ComponentModel.DataAnnotations;
namespace Model.DTOs.BackEnd.TenantMenuManage
{
    /// <summary>
    ///新增租户菜单输入类
    /// </summary>
    public class AddTenantMenuInput
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        [MaxLength(50, ErrorMessage = "NameTooLong50")]
        public string Name { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        [Required(ErrorMessage = "IconRequired")]
        [MaxLength(50, ErrorMessage = "IconTooLong50")]
        public string Icon { get; set; }

        /// <summary>
        /// Router
        /// </summary>
        [Required(ErrorMessage = "RouterRequired")]
        [MaxLength(100, ErrorMessage = "RouterTooLong100")]
        public string Router { get; set; }

        /// <summary>
        /// Component
        /// </summary>
        [Required(ErrorMessage = "ComponentRequired")]
        [MaxLength(100, ErrorMessage = "ComponentTooLong100")]
        public string Component { get; set; }

        /// <summary>
        /// Path
        /// </summary>
        [Required(ErrorMessage = "PathRequired")]
        [MaxLength(100, ErrorMessage = "PathTooLong100")]
        public string BrowserPath { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        [MaxLength(500, ErrorMessage = "RemarkTooLong500")]
        public string Remark { get; set; }

        /// <summary>
        /// IsHidden
        /// </summary>
        public bool IsHidden { get; set; } = false;

        /// <summary>
        /// DirectoryId
        /// </summary>
        [Required(ErrorMessage = "DirectoryIdRequired")]
        public long DirectoryId { get; set; }

    }

}