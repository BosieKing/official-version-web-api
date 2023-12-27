using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.RoleManage.Dto
{
    public class AddRoleInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        [MaxLength(50, ErrorMessage = "NameTooLong50")]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "RemarkTooLong500")]
        public string Remark { get; set; }
    }
}
