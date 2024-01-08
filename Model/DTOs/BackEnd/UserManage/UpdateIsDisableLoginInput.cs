using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.UserManage
{
    /// <summary>
    /// 更新是否允许登录输入类
    /// </summary>
    public class UpdateIsDisableLoginInput
    {
        /// <summary>
        /// 是否禁止登录
        /// </summary>
        public bool IsDisableLogin { get; set; } = false;

        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "IdRequired")]
        public long UserId { get; set; }
    }
}
