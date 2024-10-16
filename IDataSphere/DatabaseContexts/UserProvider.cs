namespace IDataSphere.DatabaseContexts
{
    /// <summary>
    /// 用户信息上下文
    /// </summary>
    public class UserProvider
    {
        private long _tenantId;
        private long _userId;
        private string _roleIds;
        private bool _isSuperManage;

        /// <summary> 
        /// 构造函数
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        public UserProvider(long tenantId, long userId, string roleIds, bool isSuperManage)
        {
            _tenantId = tenantId;
            _userId = userId;
            _roleIds = roleIds;
            _isSuperManage = isSuperManage;
        }

        /// <summary>
        /// 获取权限集合
        /// </summary>
        /// <returns></returns>
        public long[] GetRoleIds()
        {
            return _roleIds.Length > 0 ? _roleIds.Split(",", StringSplitOptions.TrimEntries).Select(p => long.Parse(p)).ToArray() : new long[0];
        }

        /// <summary>
        /// 是否是超管
        /// </summary>
        /// <returns></returns>
        public bool IsSuperManage()
        {
            return _isSuperManage;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        public long GetUserId()
        {
            return _userId;
        }

        /// <summary>
        /// 获取租户id
        /// </summary>
        /// <returns></returns>
        public long GetTenantId()
        {
            return _tenantId;
        }      

    }
}
