using System.ComponentModel;

namespace SharedLibrary.Enums
{
    /// <summary>
    /// 审核节点的审核策略
    /// </summary>
    public enum AuditApproveStrategyEnum
    {
        /// <summary>
        /// 自动全部审核通过
        /// </summary>
        [Description("自动全部审核通过")]
        Auto = 1,

        /// <summary>
        /// 该节点配置的审核人员全部审核通过后方可通过
        /// </summary>
       /// <remarks>这个配置就是单项流动的配置，一级审核完、则流动到第二审核、在流动到第三审核</remarks>
        [Description("该节点的审核人员全部审核通过后方可通过")]
        All = 2,

        /// <summary>
        /// 该节点等级任意的审核人员审核通过后即通过
        /// </summary>
        ///<remarks>选择该配置，则只允许有且仅有一级审核</remarks>
        [Description("该节点任意的审核人员审核通过后即通过")]
        Any = 3,
    }

    /// <summary>
    /// 审核结果
    /// </summary>
    public enum AuditResult
    {
        /// <summary>
        /// 等待审核
        /// </summary>
        [Description("等待审核")]
        Wait = 1,

        /// <summary>
        /// 审核通过
        /// </summary>     
        [Description("审核通过")]
        Succeed = 2,

        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("审核不通过")]
        Failed = 3,
    }
}