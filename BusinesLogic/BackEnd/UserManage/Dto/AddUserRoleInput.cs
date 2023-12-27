using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.UserManage.Dto
{
    public class AddUserRoleInput
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "IdRequired")]
        public long UserId { get; set; }

        /// <summary>
        /// 角色id数组
        /// </summary>
        public long[] RoleIds { get; set; }
    }
}
