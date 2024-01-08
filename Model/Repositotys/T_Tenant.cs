using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys
{
    /// <summary>
    /// 租户表
    /// </summary>
    [Table("T_Tenant")]
    public class T_Tenant : EntityBaseDO
    {
        /// <summary>
        /// 唯一编码
        /// </summary>
        [MaxLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 邀请码
        /// </summary>
        [MaxLength(200)]
        public string InviteCode { get; set; }

        /// <summary>
        /// 是否是超级管理员平台
        /// </summary>
        public bool IsSuperManage { get; set; } = false;
    }
}
