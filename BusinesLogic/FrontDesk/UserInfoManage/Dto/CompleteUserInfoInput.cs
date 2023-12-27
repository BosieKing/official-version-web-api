﻿using System.ComponentModel.DataAnnotations;

namespace BusinesLogic.FrontDesk.UserInfoManage.Dto
{
    /// <summary>
    /// 完善资料输入类
    /// </summary>
    public class CompleteUserInfoInput
    {       

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
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "SexRequried")]
        public short Sex { get; set; }

    }
}
