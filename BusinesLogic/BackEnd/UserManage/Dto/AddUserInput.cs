using System.ComponentModel.DataAnnotations;
namespace BusinesLogic.BackEnd.UserManage.Dto
{
    public class AddUserInput
    {       
        /// <summary>
        /// �绰����
        /// </summary>
        [Required(ErrorMessage = "PhoneRequired")]
        [RegularExpression(@"^1[3|4|5|7|8|9][0-9]{9}$", ErrorMessage = "PhoneFormatError")]
        public string Phone { get; set; }

        /// <summary>
        /// �û���
        /// </summary>
        [Required(ErrorMessage = "NickNameRequired")]
        [MaxLength(50, ErrorMessage = "NickNameTooLong50")]
        public string NickName { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Required(ErrorMessage = "EmailRequired")]
        [MaxLength(50, ErrorMessage = "EmailTooLong50")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$", ErrorMessage = "EmailFormatError")]
        public string Email { get; set; }
    
        /// <summary>
        /// ��Ż�ѧ��
        /// </summary>
        [MaxLength(100, ErrorMessage = "UserCodeTooLong100")]
        public string Code { get; set; }
      
        /// <summary>
        /// ����
        /// </summary>
        [Required(ErrorMessage = "PasswordRequired")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*()_+`\-={}:"";'<>,.?])[A-Za-z\d~!@#$%^&*()_+`\-={}:"";'<>,.?]{6,15}", ErrorMessage = "PasswordFormatError")]
        public string Password { get; set; } = string.Empty;       

        /// <summary>
        /// �Ա�
        /// </summary>
        [Required(ErrorMessage = "SexRequried")]
        public short Sex { get; set; }      

        /// <summary>
        /// �Ƿ��ֹ��¼
        /// </summary>
        public bool IsDisableLogin { get; set; } = false;
    }

}