namespace SharedLibrary.Consts
{
    /// <summary>
    /// 基础数据缓存库常量
    /// </summary>
    public class BasicDataCacheConst
    {
        /// <summary>
        /// 租户Hash表
        /// </summary>
        /// <remarks>Key：常量 Field：租户id Value：租户相关信息</remarks>
        public const string TENANT_TABLE = "Tenant_HashTable";

        /// <summary>
        /// 角色菜单表
        /// </summary>
        /// <remarks>Key：常量 + 租户id Field：角色id Value：菜单集合</remarks>
        public const string ROLE_TABLE = "Role_Table_";

    }
}
