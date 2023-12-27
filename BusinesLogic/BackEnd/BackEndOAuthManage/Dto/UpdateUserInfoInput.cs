using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.BackEnd.BackEndOAuthManage.Dto
{
    public class UpdateUserInfoInput
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        [Required(ErrorMessage = "RealNameRequired")]
        [MaxLength(50, ErrorMessage = "RealNameTooLong50")]
        public string RealName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage = "EmailRequired")]
        [MaxLength(50, ErrorMessage = "EmailTooLong50")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$", ErrorMessage = "EmailFormatError")]
        public string Email { get; set; }
    }
}
