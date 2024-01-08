using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.UserManage
{
    /// <summary>
    /// 用户绑定角色输入类
    /// </summary>
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
