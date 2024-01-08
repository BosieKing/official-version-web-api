using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.UserManage
{

    /// <summary>
    /// 重置密码输入类
    /// </summary>
    public class ResetPasswordInput
    {
        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "PasswordRequired")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*()_+`\-={}:"";'<>,.?])[A-Za-z\d~!@#$%^&*()_+`\-={}:"";'<>,.?]{6,15}", ErrorMessage = "PasswordFormatError")]
        public string Password { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "IdRequired")]
        public long UserId { get; set; }
    }
}
