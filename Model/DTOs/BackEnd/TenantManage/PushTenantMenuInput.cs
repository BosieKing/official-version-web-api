namespace Model.DTOs.BackEnd.TenantManage
{
    /// <summary>
    /// 推送菜单数据到租户菜单库输入类
    /// </summary>
    public class PushTenantMenuInput
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 目录id
        /// </summary>
        public long DirectoryId { get; set; }
    }
}
