using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.RoleManage
{
    /// <summary>
    /// 角色绑定菜单输入类
    /// </summary>
    public class AddRoleMenuInput
    {
        /// <summary>
        /// 角色id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long RoleId { get; set; }

        /// <summary>
        /// 菜单id集合
        /// </summary>
        public long[] MenuIds { get; set; }
    }
}
