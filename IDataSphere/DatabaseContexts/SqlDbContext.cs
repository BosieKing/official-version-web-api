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
                // 否则默认是继承的是最高级父类
                 if (entity.Entity.GetType().IsSubclassOf(typeof(EntityBaseDO)))
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
            List<Type> types = assembly.GetTypes().Where(p => p.IsClass && !p.IsInterface && !p.IsSealed && p.Namespace.EndsWith("Repositotys") && p.Name != nameof(EntityBaseDO) )
                                                  .Where(p => p.BaseType.Name == nameof(EntityBaseDO))
                                                  .ToList();
            types.ForEach(item =>
            {
                modelBuilder.Entity(item).HasQueryFilter(FakeDeleteQueryFilterExpression(item));
            });
            return modelBuilder;
        }

        /// <summary>
        /// 构建全局过滤查询
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private LambdaExpression FakeDeleteQueryFilterExpression(Type type)
        {
            ParameterExpression p = Expression.Parameter(type, "p");
            BinaryExpression fakeDeleteExpression = CreateFakeDeleteExpression(type, nameof(EntityBaseDO.IsDeleted), p);
            LambdaExpression result = null;
            result = Expression.Lambda(fakeDeleteExpression, p);
            return result;
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
