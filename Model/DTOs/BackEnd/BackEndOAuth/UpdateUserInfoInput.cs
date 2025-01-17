﻿using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.BackEndOAuth
{
    /// <summary>
    /// 个人中心修改个人信息输入类
    /// </summary>
    public class UpdateUserInfoInput
    {
        /// <summary>
        /// 昵称
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
    }
}
