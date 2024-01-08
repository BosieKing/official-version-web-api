using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.TenantMenuManage
{
    /// <summary>
    /// 新增租户菜单按钮输入类
    /// </summary>
    public class AddTenantMenuButtonInput
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long MenuId { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        [MaxLength(50, ErrorMessage = "NameTooLong50")]
        public string Name { get; set; }


        /// <summary>
        /// 按钮对应方法名称
        /// </summary>
        [Required(ErrorMessage = "ActionNameRequired")]
        [MaxLength(50, ErrorMessage = "ActionNameTooLong50")]
        public string ActionName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "RemarkTooLong500")]
        public string Remark { get; set; }
    }
}
