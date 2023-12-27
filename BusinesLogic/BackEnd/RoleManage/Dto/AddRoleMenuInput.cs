using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.RoleManage.Dto
{
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
