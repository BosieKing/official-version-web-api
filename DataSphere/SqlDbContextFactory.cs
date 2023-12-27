using Microsoft.EntityFrameworkCore;


namespace DataSphere
{
    /// <summary>
    /// DMDbContext工厂
    /// </summary>
    public class SqlDbContextFactory : IDbContextFactory<SqlDbContext>
    {
        private readonly IDbContextFactory<SqlDbContext> _pooledFactory;
        private readonly long _tenantId;
        private readonly long _userId;

        public SqlDbContextFactory(
            IDbContextFactory<SqlDbContext> pooledFactory,
            UserProvider userProvider)
        {
            _pooledFactory = pooledFactory;
            _tenantId = userProvider.GetTenantId();
            _userId = userProvider.GetUserId();
        }

        /// <summary>
        /// 创建DMDbContext
        /// </summary>
        /// <returns></returns>
        public SqlDbContext CreateDbContext()
        {
            var context = _pooledFactory.CreateDbContext();
            context.TenantId = _tenantId;
            context.UserId = _userId;
            return context;
        }
    }
}
