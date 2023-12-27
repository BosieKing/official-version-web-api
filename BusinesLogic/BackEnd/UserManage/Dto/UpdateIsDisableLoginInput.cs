using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.UserManage.Dto
{
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
