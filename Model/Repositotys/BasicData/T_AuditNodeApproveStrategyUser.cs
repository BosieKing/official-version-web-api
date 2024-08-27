using System.ComponentModel.DataAnnotations.Schema;
namespace Model.Repositotys.BasicData
{
    /// <summary>
    /// 根据审核节点策略
    /// </summary>
    [Table(nameof(T_AuditNodeApproveStrategyUser))]
    public class T_AuditNodeApproveStrategyUser : EntityTenantDO
    {
        /// <summary>
        /// 归属具体
        /// </summary>
        public long AuditNodeId { get; set; }

        /// <summary>
        /// 归属具体节点
        /// </summary>
        public long AuditNodeConfigOptionId { get; set; }

        /// <summary>
        /// 审核人id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 审核结果
        /// </summary>
        /// <see cref="SharedLibrary.Enums.AuditResult"/>
        public int Result { get; set; }
    }
}
