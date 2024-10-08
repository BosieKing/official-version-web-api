﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 用户角色中间表
    /// </summary>
    [Table(nameof(T_UserRole))]
    public class T_UserRole : EntityTenantDO
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long UserId { get; set; }
    }
}
