using Model.Commons.SharedData;
using System.ComponentModel.DataAnnotations;
namespace Model.DTOs.BackEnd.TenantMenuManage
{
    /// <summary>
    /// 查询租户表格数据输入类
    /// </summary>
    public class GetTenantMenuPageInput : PageInput
    {
        /// <summary>
        /// Name
        /// </summary>
        [MaxLength(50, ErrorMessage = "NameTooLong50")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Icon
        /// </summary>
        [MaxLength(50, ErrorMessage = "IconTooLong50")]
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// Router
        /// </summary>
        [MaxLength(100, ErrorMessage = "RouterTooLong100")]
        public string Router { get; set; } = string.Empty;

        /// <summary>
        /// Component
        /// </summary>
        [MaxLength(100, ErrorMessage = "ComponentTooLong100")]
        public string Component { get; set; } = string.Empty;

        /// <summary>
        /// Path
        /// </summary>
        [MaxLength(100, ErrorMessage = "PathTooLong100")]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Weight
        /// </summary>
        public int Weight { get; set; } = 0;

        /// <summary>
        /// Remark
        /// </summary>
        [MaxLength(500, ErrorMessage = "RemarkTooLong500")]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// IsHidden
        /// </summary>
        public bool? IsHidden { get; set; }

        /// <summary>
        /// DirectoryId
        /// </summary>
        public long DirectoryId { get; set; } = 0;

        /// <summary>
        /// UniqueNumber
        /// </summary>
        public long UniqueNumber { get; set; } = 0;
    }

}