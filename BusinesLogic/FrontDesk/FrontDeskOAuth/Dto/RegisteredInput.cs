using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.FrontDesk.FrontDeskOAuth.Dto
{
    /// <summary>
    /// 注册输入类
    /// </summary>
    public class RegisteredInput
    {
        /// <summary>
        /// 单位邀请码
        /// </summary>
        [Required(ErrorMessage = "InviteCodeRequired")]
        public string InviteCode { get; set; } = string.Empty;       

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "NickNameRequired")]
        [MaxLength(50, ErrorMessage = "NickNameTooLong50")]
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 电话号码
        /// </summary>
        [Required(ErrorMessage = "PhoneRequired")]
        [RegularExpression(@"^1[3|4|5|7|8|9][0-9]{9}$", ErrorMessage = "PhoneFormatError")]
        public string Phone { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "PasswordRequired")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*()_+`\-={}:"";'<>,.?])[A-Za-z\d~!@#$%^&*()_+`\-={}:"";'<>,.?]{6,15}", ErrorMessage = "PasswordFormatError")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "VerifCodeRequired")]
        public string VerifyCode { get; set; } = string.Empty;
    }
}
