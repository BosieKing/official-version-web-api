﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDataSphere.Repositoty
{
    /// <summary>
    /// 权限表
    /// </summary>
    [Table("T_Role")]
    public class T_Role : EntityTenantDO
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
