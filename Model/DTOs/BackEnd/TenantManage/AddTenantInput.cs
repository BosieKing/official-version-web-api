using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.TenantManage
{
    /// <summary>
    /// 新增租户输入类
    /// </summary>
    public class AddTenantInput
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        [MaxLength(50, ErrorMessage = "NameTooLong50")]
        public string Name { get; set; }

        /// <summary>
        /// 租户编号
        /// </summary>
        [Required(ErrorMessage = "CodeRequired")]
        [MaxLength(50, ErrorMessage = "CodeTooLong50")]
        public string Code { get; set; }

        /// <summary>
        ///  昵称
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
    }
}
