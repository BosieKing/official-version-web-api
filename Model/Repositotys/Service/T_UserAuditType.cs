using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 用户审核身份中间表
    /// </summary>
    [Table(nameof(T_UserAuditType))]
    public class T_UserAuditType : EntityTenantDO
    {
        /// <summary>
        /// 审核角色
        /// </summary>
        public long AuditTypeId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long UserId { get; set; }
    }
}
