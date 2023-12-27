using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.UserManage.Dto
{
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
