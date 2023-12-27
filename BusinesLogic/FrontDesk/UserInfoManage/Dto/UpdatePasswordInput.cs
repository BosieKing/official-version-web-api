using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.FrontDesk.UserInfoManage.Dto
{
    /// <summary>
    /// 修改密码输入类
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
