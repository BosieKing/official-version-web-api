using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model.Repositotys;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Yitter.IdGenerator;

namespace IDataSphere.DatabaseContexts
{
    /// <summary>
    /// Sql数据库上下文类
    /// </summary>
    public partial class SqlDbContext : DbContext
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
            EntityInjection(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 重写保存
        /// </summary>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            // 返回跟踪实体的最新状态
            var entityList = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified || p.State == EntityState.Deleted || p.State == EntityState.Added)
                                                    .Select(p => p).ToList();

            // 获取操作人信息
            foreach (var entity in entityList)
            {
                // 继承租户基类
                bool baseTypeIsTenant = entity.GetType().BaseType.Name == nameof(EntityTenantDO);
                if (baseTypeIsTenant)
                {
                    var obj = entity.Entity as EntityTenantDO;
                    switch (entity.State)
                    {
                        case EntityState.Added:
                            // 判断id
                            obj.Id = obj.Id == 00 ? YitIdHelper.NextId() : obj.Id;
                            // 设置创建人和创建时间
                            obj.CreatedTime = DateTime.Now;
                            if (!UserId.Equals(0) && obj.CreatedUserId.Equals(0))
                            {
                                obj.CreatedUserId = UserId;
                            }
                            // 如果手动设置了租户则不从添加人内获取,否则设置租户id
                            if (!TenantId.Equals(0) && obj.TenantId.Equals(0))
                            {
                                obj.TenantId = TenantId;
                            }
                            break;
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            //  排除租户Id
                            entity.Property(nameof(EntityTenantDO.TenantId)).IsModified = false;
                            // 排除创建人
                            entity.Property(nameof(EntityTenantDO.CreatedUserId)).IsModified = false;
                            // 排除创建日期
                            entity.Property(nameof(EntityTenantDO.CreatedTime)).IsModified = false;
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
                // 否则默认是继承的是最高级父类
                else
                {
                    var obj = entity.Entity as EntityBaseDO;
                    switch (entity.State)
                    {
                        case EntityState.Added:
                            // 判断id
                            obj.Id = obj.Id == 00 ? YitIdHelper.NextId() : obj.Id;
                            // 设置创建人和创建时间
                            obj.CreatedTime = DateTime.Now;
                            if (!UserId.Equals(0) && obj.CreatedUserId.Equals(0))
                            {
                                obj.CreatedUserId = UserId;
                            }
                            break;
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            // 排除创建人
                            entity.Property(nameof(EntityBaseDO.CreatedUserId)).IsModified = false;
                            // 排除创建日期
                            entity.Property(nameof(EntityBaseDO.CreatedTime)).IsModified = false;
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
        private ModelBuilder EntityInjection(ModelBuilder modelBuilder)
        {
            Assembly assembly = Assembly.Load("Model");
            List<Type> types = assembly.GetTypes().Where(p => p.IsClass && !p.IsInterface && !p.IsSealed && p.Namespace.EndsWith("Repositoty") && p.Name != nameof(EntityBaseDO) && p.Name != nameof(EntityTenantDO))
                                                  .Where(p => p.BaseType.Name == nameof(EntityBaseDO) || p.BaseType.Name == nameof(EntityTenantDO))
                                                  .ToList();
            types.ForEach(item =>
            {
                modelBuilder.Entity(item).HasQueryFilter(TenantIdAndFakeDeleteQueryFilterExpression(item));
            });
            return modelBuilder;
        }

        /// <summary>
        /// 构建全局过滤查询
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private LambdaExpression TenantIdAndFakeDeleteQueryFilterExpression(Type type)
        {
            ParameterExpression p = Expression.Parameter(type, "p");
            BinaryExpression fakeDeleteExpression = CreateFakeDeleteExpression(type, nameof(EntityTenantDO.IsDeleted), p);
            LambdaExpression result = null;
            switch (type.BaseType.Name)
            {
                case nameof(EntityBaseDO):
                    result = Expression.Lambda(fakeDeleteExpression, p);
                    break;
                case nameof(EntityTenantDO):
                    BinaryExpression tenantExpression = CreateTenantIdExpression(type, nameof(EntityTenantDO.TenantId), p);
                    result = Expression.Lambda(Expression.AndAlso(fakeDeleteExpression, tenantExpression), p);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 构建多租户过滤器
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private BinaryExpression CreateTenantIdExpression(Type type, string fieldName, ParameterExpression p)
        {
            // 构建成员表达式 p.TenantId
            MemberExpression memberExpression = Expression.PropertyOrField(p, fieldName);
            // 传递对象设置常量
            ConstantExpression dbContextConstant = Expression.Constant(this, typeof(SqlDbContext));
            // 得到数据库上下文的属性访问器
            MemberExpression dbTenantId = Expression.PropertyOrField(dbContextConstant, nameof(TenantId));
            // 构建关系
            return Expression.MakeBinary(ExpressionType.Equal, memberExpression, dbTenantId);
        }

        /// <summary>
        /// 构建加删除过滤器
        /// </summary>
        /// <param name="p"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private BinaryExpression CreateFakeDeleteExpression(Type type, string fieldName, ParameterExpression p)
        {
            // 构建p.IsDelete
            MemberExpression memberExpression = Expression.PropertyOrField(p, fieldName);
            // 设置默认常量
            ConstantExpression constantExpression = Expression.Constant(false);
            // 构建关系
            return Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression);
        }
        #endregion

    }
}
