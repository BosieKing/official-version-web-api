﻿using Model.Repositotys.BasicData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("T_User")]
    public class T_User : EntityTenantDO
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(50)]
        public string NickName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [MaxLength(50)]
        public string Phone { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(200)]
        public string Password { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        public short Sex { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        [MaxLength(200)]
        public string? AvatarUrl { get; set; } = string.Empty;

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// 是否禁止登录
        /// </summary>
        public bool IsDisableLogin { get; set; } = false;

        /// <summary>
        /// 角色组
        /// </summary>
        public List<T_Role> Roles { get; set; }

        /// <summary>
        /// 审核角色组
        /// </summary>
        public List<T_AuditType> AuditTypes { get; set; }
    }
}
