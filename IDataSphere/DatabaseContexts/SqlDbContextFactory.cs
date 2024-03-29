﻿using Microsoft.EntityFrameworkCore;

namespace IDataSphere.DatabaseContexts
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class SqlDbContextFactory : IDbContextFactory<SqlDbContext>
    {
        /// <summary>
        /// 数据库工厂
        /// </summary>
        private readonly IDbContextFactory<SqlDbContext> _pooledFactory;
        private readonly long _tenantId;
        private readonly long _userId;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pooledFactory"></param>
        /// <param name="userProvider"></param>
        public SqlDbContextFactory(
            IDbContextFactory<SqlDbContext> pooledFactory,
            UserProvider userProvider)
        {
            _pooledFactory = pooledFactory;
            _tenantId = userProvider.GetTenantId();
            _userId = userProvider.GetUserId();
        }

        /// <summary>
        /// 创建DbContext
        /// </summary>
        /// <returns></returns>
        public SqlDbContext CreateDbContext()
        {
            SqlDbContext context = _pooledFactory.CreateDbContext();
            context.TenantId = _tenantId;
            context.UserId = _userId;
            return context;
        }
    }
}
