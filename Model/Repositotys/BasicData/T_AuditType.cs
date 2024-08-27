using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.BasicData
{
    /// <summary>
    /// 审核角色类型表
    /// </summary>
    [Table(nameof(T_AuditType))]
    public class T_AuditType : EntityTenantDO
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
