namespace SharedLibrary.Consts
{
    /// <summary>
    /// Token中的自定义Claim属性
    /// </summary>
    public class ClaimsUserConst
    {
        /// <summary>
        /// token中的用户字段
        /// </summary>
        public const string USER_ID = "UserId";

        /// <summary>
        /// token是否延迟过期--刷新token专用
        /// </summary>
        public const string IS_REMEMBER = "IsRemember";

        /// <summary>
        /// token中的角色字段
        /// </summary>
        public const string ROLE_IDs = "RoleIds";

        /// <summary>
        /// token中的租户字段
        /// </summary>
        public const string TENANT_ID = "TenantId";

        /// <summary>
        /// token中的SchemeName字段，用于标识不同租户业务的切换
        /// </summary>
        public const string SCHEME_NAME = "SchemeName";

        /// <summary>
        /// 当为超管的时候，颁发的token会携带此字段
        /// </summary>
        public const string IS_SUPERMANAGE = "IsSuperManage";

        /// <summary>
        /// 在http请求中，token存放的位置
        /// </summary>
        public const string HTTP_Token_Head = "Authorization";

        /// <summary>
        /// 在http请求中，刷新token存放的位置
        /// </summary>
        public const string HTTP_REFRESHToken_Head = "X-Authorization";
    }
}

