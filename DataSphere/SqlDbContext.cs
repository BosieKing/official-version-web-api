using IDataSphere.Interface;
using IDataSphere.Repositoty;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Yitter.IdGenerator;

namespace DataSphere
{
    /// <summary>
    /// 达梦数据库上下文类
    /// </summary>
    public partial class SqlDbContext : DbContextAbstract
    {
        #region 构造函数
        /// <summary>
        /// 租户Id
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 传递给父类
        /// </summary>
        /// <param name="options"></param>
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {

        }
        #endregion

        #region 重写事件             

        /// <summary>
        /// 模型创建
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 选择需要注册的表
            DynamicDbSet(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            // 返回跟踪实体的最新状态
            List<EntityEntry> entityList = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified || p.State == EntityState.Deleted || p.State == EntityState.Added)
                                                                  .Select(p => p).ToList();
            // 获取操作人信息
            foreach (var item in entityList)
            {
                // 如果实体继承的是实现租户父类
                if (item.Entity.GetType().IsSubclassOf(typeof(EntityTenantDO)))
                {
                    var obj = item.Entity as EntityTenantDO;
                    switch (item.State)
                    {
                        case EntityState.Added:
                            // 判断id
                            obj.Id = obj.Id == 00 ? YitIdHelper.NextId() : obj.Id;
                            // 设置创建人和创建时间
                            obj.CreatedTime = DateTime.Now;
                            if (!UserId.Equals(0))
                            {
                                obj.CreatedUserId = UserId;
                            }
                            // 如果手动设置了租户则不从添加人内获取,否则设置租户id
                            if (!TenantId.Equals(0) && obj.TenantId.Equals(0))
                            {
                                obj.TenantId = TenantId;
                            }
                            break;
                        case EntityState.Deleted:
                        case EntityState.Modified:
                            //  排除租户Id
                            item.Property(nameof(EntityTenantDO.TenantId)).IsModified = false;
                            // 排除创建人
                            item.Property(nameof(EntityTenantDO.CreatedUserId)).IsModified = false;
                            // 排除创建日期
                            item.Property(nameof(EntityTenantDO.CreatedTime)).IsModified = false;
                            if (!UserId.Equals(0))
                            {
                                obj.UpdateUserId = UserId;
                            }
                            obj.UpdateTime = DateTime.Now;
                            break;
                        default:
                            break;
                    }
                }
                // 如果继承的是最高级父类
                else if (item.Entity.GetType().IsSubclassOf(typeof(EntityBaseDO)))
                {
                    var obj = item.Entity as EntityBaseDO;
                    switch (item.State)
                    {
                        case EntityState.Added:
                            // 判断id
                            obj.Id = obj.Id == 00 ? YitIdHelper.NextId() : obj.Id;
                            // 设置创建人和创建时间
                            obj.CreatedTime = DateTime.Now;
                            obj.CreatedUserId = UserId;
                            break;
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            // 排除创建人
                            item.Property(nameof(EntityBaseDO.CreatedUserId)).IsModified = false;
                            // 排除创建日期
                            item.Property(nameof(EntityBaseDO.CreatedTime)).IsModified = false;
                            if (!UserId.Equals(0))
                            {
                                obj.UpdateUserId = UserId;
                            }
                            obj.UpdateTime = DateTime.Now;
                            break;
                        default:
                            break;
                    }
                }
            }
            return base.SaveChangesAsync();
        }

        #endregion

        #region 辅助函数
        /// <summary>
        /// 注入实体，附加过滤查询
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <returns></returns>
        private ModelBuilder DynamicDbSet(ModelBuilder modelBuilder)
        {
            // 找到IDao的Repositoty
            Assembly assembly = Assembly.Load("IDao");
            List<Type> types = assembly.GetTypes().Where(p => p.IsClass && !p.IsInterface && !p.IsSealed && p.Namespace.EndsWith("Repositoty")).ToList();
            foreach (var item in types)
            {
                if (item.Name == nameof(EntityBaseDO) || item.Name == nameof(EntityTenantDO))
                {
                    continue;
                }
                modelBuilder.Entity(item).HasQueryFilter(TenantIdAndFakeDeleteQueryFilterExpression(item));
            }
            return modelBuilder;
        }

        /// <summary>
        /// 构建全局过滤查询
        /// </summary>
        /// <param name="entityBuilder"></param>
        private LambdaExpression TenantIdAndFakeDeleteQueryFilterExpression(Type type)
        {
            ParameterExpression p = Expression.Parameter(type, "p");
            BinaryExpression deleteExpression = null;
            if (type.GetProperty(nameof(EntityBaseDO.IsDeleted)) != null)
            {
                deleteExpression = GetFakeDeleteExpression(type, nameof(EntityBaseDO.IsDeleted), p);
            }
            BinaryExpression tenantIdExpression = null;
            if (type.GetProperty(nameof(EntityTenantDO.TenantId)) != null)
            {
                tenantIdExpression = GetTenantIdExpression(type, nameof(EntityTenantDO.TenantId), p);
            }
            if (deleteExpression != null && tenantIdExpression != null)
            {
                return Expression.Lambda(Expression.AndAlso(deleteExpression, tenantIdExpression), p);
            }
            return Expression.Lambda(deleteExpression, p);
        }

        /// <summary>
        /// 构建多租户过滤器
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private BinaryExpression GetTenantIdExpression(Type type, string fieldName, ParameterExpression p)
        {
            // 构建成员表达式 p.TenantId
            MemberExpression memberExpression = Expression.PropertyOrField(p, fieldName);
            // 设置d常量
            ConstantExpression dbContextConstant = Expression.Constant(this, typeof(SqlDbContext));
            // 得到d.TenantId
            MemberExpression dbTenantId = Expression.PropertyOrField(dbContextConstant, nameof(TenantId));
            // 构建成员与方法的关系
            BinaryExpression binaryExpression = Expression.MakeBinary(ExpressionType.Equal, memberExpression, dbTenantId);
            return binaryExpression;
        }

        /// <summary>
        /// 构建加删除过滤器
        /// </summary>
        /// <param name="p"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private BinaryExpression GetFakeDeleteExpression(Type type, string fieldName, ParameterExpression p)
        {
            // 构建p.IsDelete
            MemberExpression memberExpression = Expression.PropertyOrField(p, fieldName);
            // 构建false
            ConstantExpression constantExpression = Expression.Constant(false);
            // 构建关系
            BinaryExpression binaryExpression = Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression);
            return binaryExpression;
        }
        #endregion

    }
}
