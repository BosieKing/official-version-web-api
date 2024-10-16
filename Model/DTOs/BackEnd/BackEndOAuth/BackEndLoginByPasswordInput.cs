using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.BackEndOAuth
{
    /// <summary>
    /// 后台忘记密码输入类
    /// </summary>
    public class BackEndLoginByPasswordInput
    {
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "PasswordRequired")]
        public string Password { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        [Required(ErrorMessage = "PhoneRequired")]
        [RegularExpression(@"^1[3|4|5|7|8|9][0-9]{9}$", ErrorMessage = "PhoneFormatError")]
        public string Phone { get; set; }

        /// <summary>
        /// 验证码随机值
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 滑动验证码值
        /// </summary>
        [Required(ErrorMessage = "GraphicCaptchaRequired")]
        public string GraphicCaptcha { get; set; }

        /// <summary>
        /// 7天免登录
        /// </summary>
        public bool IsRemember { get; set; }
    }
}
