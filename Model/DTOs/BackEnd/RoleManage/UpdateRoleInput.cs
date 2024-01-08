using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.RoleManage
{
    /// <summary>
    /// 更新角色信息输入类
    /// </summary>
    public class UpdateRoleInput : AddRoleInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
}
