using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.TenantManage
{
    /// <summary>
    /// 新增租户输入类
    /// </summary>
    public class AddTenantInput
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        [MaxLength(50, ErrorMessage = "NameTooLong50")]
        public string Name { get; set; }

        /// <summary>
        /// 租户编号
        /// </summary>
        [Required(ErrorMessage = "CodeRequired")]
        [MaxLength(50, ErrorMessage = "CodeTooLong50")]
        public string Code { get; set; }
    }
}
