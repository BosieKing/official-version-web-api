using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.FrontDesk.UserInfoManage.Dto
{
    /// <summary>
    /// 导师注册输入类
    /// </summary>
    public class MentorRegisterInput
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        [Required(ErrorMessage = "RealNameRequired")]
        [MaxLength(50, ErrorMessage = "RealNameTooLong50")]
        public string RealName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Required(ErrorMessage = "PhoneRequired")]
        [RegularExpression(@"^1[3|4|5|7|8|9][0-9]{9}$", ErrorMessage = "PhoneFormatError")]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage = "EmailRequired")]
        [MaxLength(50, ErrorMessage = "EmailTooLong50")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$", ErrorMessage = "EmailFormatError")]
        public string Email { get; set; }

        /// <summary>
        /// 人员类型
        /// </summary>
        [Required(ErrorMessage = "PersonTypeIdRequired")]
        public long PersonTypeId { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        [Required(ErrorMessage = "DepartmentIdRequired")]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 身份类型
        /// </summary>
        [Required(ErrorMessage = "IdentityTypeIdRequired")]
        public long IdentityTypeId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "SexRequried")]
        public short Sex { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "VerifCodeRequired")]
        public string VerifyCode { get; set; }
    }
}
