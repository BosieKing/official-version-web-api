using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.FronDesk.UserInfoManage
{
    /// <summary>
    /// 通过原密码更新密码输入类
    /// </summary>
    public class UpdatePasswordInput
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [Required(ErrorMessage = "OldPasswordRequired")]
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "NewPasswordRequired")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*()_+`\-={}:"";'<>,.?])[A-Za-z\d~!@#$%^&*()_+`\-={}:"";'<>,.?]{6,15}", ErrorMessage = "NewPasswordFormatError")]
        public string NewPassword { get; set; }
    }
}
