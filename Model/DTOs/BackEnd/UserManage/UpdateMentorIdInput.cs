using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.UserManage
{
    /// <summary>
    /// 绑定导师输入类
    /// </summary>
    public class UpdateMentorIdInput
    {
        /// <summary>
        /// 导师id
        /// </summary>
        [Required(ErrorMessage = "MentorIdRequired")]
        public long MentorId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "IdRequired")]
        public long UserId { get; set; }

    }
}
