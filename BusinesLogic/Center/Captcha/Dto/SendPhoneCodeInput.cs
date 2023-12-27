using System.ComponentModel.DataAnnotations;


namespace BusinesLogic.Center.Captcha.Dto
{
    /// <summary>
    /// 发送验证码
    /// </summary>
    public class SendPhoneCodeInput
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required(ErrorMessage = "PhoneRequired")]
        [RegularExpression(@"^1[3|4|5|7|8|9][0-9]{9}$", ErrorMessage = "PhoneFormatError")]
        public string Phone { get; set; }
    }
}
