namespace IDataSphere.DatabaseContexts
{
    /// <summary>
    /// 用户信息上下文
    /// </summary>
    public class UserProvider
    {
        private long _tenantId;
        private long _userId;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        public UserProvider(long tenantId, long userId)
        {
            _tenantId = tenantId;
            _userId = userId;
        }

        /// <summary>
        /// 获取租户id
        /// </summary>
        /// <returns></returns>
        public long GetTenantId()
        {
            return _tenantId;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        public long GetUserId()
        {
            return _userId;
        }
    }
}
