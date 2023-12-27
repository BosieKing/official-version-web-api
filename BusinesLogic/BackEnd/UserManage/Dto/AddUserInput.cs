using System.ComponentModel.DataAnnotations;
namespace BusinesLogic.BackEnd.UserManage.Dto
{
    public class AddUserInput
    {       
        /// <summary>
        /// 电话号码
        /// </summary>
        [Required(ErrorMessage = "PhoneRequired")]
        [RegularExpression(@"^1[3|4|5|7|8|9][0-9]{9}$", ErrorMessage = "PhoneFormatError")]
        public string Phone { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "NickNameRequired")]
        [MaxLength(50, ErrorMessage = "NickNameTooLong50")]
        public string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage = "EmailRequired")]
        [MaxLength(50, ErrorMessage = "EmailTooLong50")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$", ErrorMessage = "EmailFormatError")]
        public string Email { get; set; }
    
        /// <summary>
        /// 编号或学号
        /// </summary>
        [MaxLength(100, ErrorMessage = "UserCodeTooLong100")]
        public string Code { get; set; }
      
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "PasswordRequired")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*()_+`\-={}:"";'<>,.?])[A-Za-z\d~!@#$%^&*()_+`\-={}:"";'<>,.?]{6,15}", ErrorMessage = "PasswordFormatError")]
        public string Password { get; set; } = string.Empty;       

        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "SexRequried")]
        public short Sex { get; set; }      

        /// <summary>
        /// 是否禁止登录
        /// </summary>
        public bool IsDisableLogin { get; set; } = false;
    }

}