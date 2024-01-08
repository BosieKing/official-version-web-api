using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.FronDesk.UserInfoManage
{
    /// <summary>
    /// 前台验证码更新密码输入类
    /// </summary>
    public class UpdatePasswordByCodeInput
    {
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "VerifCodeRequired")]
        public string VerifyCode { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "NewPasswordRequired")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*()_+`\-={}:"";'<>,.?])[A-Za-z\d~!@#$%^&*()_+`\-={}:"";'<>,.?]{6,15}", ErrorMessage = "NewPasswordFormatError")]
        public string NewPassword { get; set; }
    }
}
