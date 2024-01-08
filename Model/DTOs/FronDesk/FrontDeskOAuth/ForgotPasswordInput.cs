using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.FronDesk.FrontDeskOAuth
{
    /// <summary>
    /// 前台忘记密码输入类
    /// </summary>
    public class ForgotPasswordInput
    {
        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "PasswordRequired")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*()_+`\-={}:"";'<>,.?])[A-Za-z\d~!@#$%^&*()_+`\-={}:"";'<>,.?]{6,15}", ErrorMessage = "PasswordFormatError")]
        public string NewPassWord { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Required(ErrorMessage = "PhoneRequired")]
        [RegularExpression(@"^1[3|4|5|7|8|9][0-9]{9}$", ErrorMessage = "PhoneFormatError")]
        public string Phone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "VerifCodeRequired")]
        public string VerifyCode { get; set; }
    }
}
