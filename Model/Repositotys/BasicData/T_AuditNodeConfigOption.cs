using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.BasicData
{
    /// <summary>
    /// 审核节点具体审核表
    /// </summary>
    [Table(nameof(T_AuditNodeConfigOption))]
    public class T_AuditNodeConfigOption : EntityTenantDO
    {
        /// <summary>
        /// 属于审核的id
        /// </summary>
        public long AuditNodeConfigId { get; set; }

        /// <summary>
        /// 第几级
        /// </summary>
        public int AuditLevel { get; set; }

        /// <summary>
        /// 审核人数据来源
        /// </summary>
        public long AuditType { get; set; }

        /// <summary>
        /// 审核人数量
        /// </summary>
        /// <remarks>当填写为0的时候，默认为需要全体该审核类型的用户全部通过后才进入下级审核，大于0则从系统默认随机找n个</remarks>
        public int AuditUserCount { get; set; }

        /// <summary>
        /// 审核策略
        /// </summary>
        /// <see cref="SharedLibrary.Enums.AuditApproveStrategyEnum"/>
        public int ApproveStrategy { get; set; }

        /// <summary>
        /// 当处理结果为不通过的时候，需要回退的分支
        /// </summary>
        /// <remarks>如到达第三级审核，这里填3，则审核不通过，修改人重新修改再次提交则继续到3级审核，前面无需再审</remarks>
        public int FailRetrunLevel { get; set; }

    }
}
