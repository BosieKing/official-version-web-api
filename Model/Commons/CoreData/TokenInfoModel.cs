namespace Model.Commons.CoreData
{
    /// <summary>
    /// Token携带信息模型
    /// </summary>
    public class TokenInfoModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 角色id集合
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 租户Code
        /// </summary>
        public string SchemeName { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string UniqueNumber { get; set; }

    }
}
