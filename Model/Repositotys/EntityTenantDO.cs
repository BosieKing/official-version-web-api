using Model.Repositotys.Service;

namespace Model.Repositotys
{
    /// <summary>
    /// 租户基类
    /// </summary>
    public class EntityTenantDO : EntityBaseDO
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        public T_Tenant Tenant { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public long UpdateUserId { get; set; } = 0;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
