using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.BasicData
{
    /// <summary>
    /// 审核节点配置
    /// </summary>
    [Table(nameof(T_AuditNodeConfig))]
    public class T_AuditNodeConfig : EntityTenantDO
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 第几级
        /// </summary>
        public int NodeLevel { get; set; } 

        /// <summary>
        /// 审核类型
        /// </summary>
        public int AuditNodeConfigType { get; set; }
    }
}
