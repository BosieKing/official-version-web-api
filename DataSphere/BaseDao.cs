using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.Repositotys;
using System.Linq.Expressions;
using UtilityToolkit.Utils;
using Yitter.IdGenerator;

namespace DataSphere
{
    /// <summary>
    /// 自定义操作数据库方法
    /// </summary>
    public class BaseDao : IBaseDao
    {
        #region 构造函数   
        public readonly SqlDbContext dbContext;
        public BaseDao(SqlDbContext dMDbContext)
        {
            this.dbContext = dMDbContext;
        }

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        public SqlDbContext GetDBContext()
        {
            return dbContext;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        /// <returns></returns>
        public long UserId()
        {
            return dbContext.UserId;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        /// <returns></returns>
        public long TenantId()
        {
            return dbContext.TenantId;
        }

        /// <summary>
        /// 获取指定Rep
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="isIgnoreTenant"></param>
        /// <returns></returns>
        public DbSet<TEntity> GetRep<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            return rep;
        }
        #endregion

        #region 判断
        /// <summary>
        /// 根据条件判断数据是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DataExisted<TEntity>(Expression<Func<TEntity, bool>> expression, bool isIgnoreTenant = false) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            if (isIgnoreTenant)
            {
                return await rep.IgnoreQueryFilters().Where(p => p.IsDeleted == false).AnyAsync(expression);
            }
            else
            {
                return await rep.AnyAsync(expression);
            }
            
        }

        /// <summary>
        /// 判断Id是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IdExisted<TEntity>(long id) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            return await rep.AnyAsync(p => p.Id == id);
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 搜索条件模型自动附加单表分页查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="Input"></typeparam>
        /// <param name="input"></param>
        /// <param name="isIgnoreTenant"></param>
        /// <returns></returns>
        public async Task<PageResult> GetPage<TEntity, Input>(Input input, bool isIgnoreTenant = false) where TEntity : EntityBaseDO where Input : PageInput
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            IQueryable<TEntity> query = rep.AddSearchCriteria(input);
            if (isIgnoreTenant)
            {
                int count = await query.IgnoreTenantFilter().CountAsync();
                List<TEntity> data = await query.IgnoreTenantFilter().Select(p => p).Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize).ToListAsync();
                return new PageResult(input.PageNo, input.PageSize, count, data);
            }
            else
            {
                int count = await query.CountAsync();
                List<TEntity> data = await query.Select(p => p).Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize).ToListAsync();
                return new PageResult(input.PageNo, input.PageSize, count, data);
            }
        }

        /// <summary>
        /// 查询语句自动转为分页
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public async Task<PageResult> AdaptPage<TEntity>(IQueryable<TEntity> query, int pageSize, int pageNo)
        {
            int count = await query.CountAsync();
            List<TEntity> data = await query.Select(p => p).Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageResult(pageNo, pageSize, count, data);
        }
        #endregion

        #region 下拉列表
        /// <summary>
        ///  获取下拉列表，采集int类型字段
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="Int"></typeparam>
        /// <param name="expression"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public async Task<List<(long Id, int Field)>> GetIntList<TEntity, Int>(Expression<Func<TEntity, bool>> expression, string fieldName) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            Type resultType = typeof(ValueTuple<long, int>);
            // 参数表达式，构建P
            ParameterExpression p = Expression.Parameter(typeof(TEntity), "p");
            // 成员表达式，构建 p.id
            MemberExpression idMemberExpression = Expression.PropertyOrField(p, "Id");
            MemberExpression memberExpression = Expression.PropertyOrField(p, fieldName);
            // 赋值表达式，构建 Id => p.id
            var fields = resultType.GetFields();
            MemberAssignment idMemberAssignment = Expression.Bind(fields[0], idMemberExpression);
            MemberAssignment memberAssignment = Expression.Bind(fields[1], memberExpression);
            // 规范返回模板
            NewExpression result = Expression.New(typeof((long Id, int Field)));
            // 按照指定规范返回模板和赋值表达式填充
            MemberInitExpression memberInitExpression = Expression.MemberInit(result, idMemberAssignment, memberAssignment);
            // 转换为查询表达式
            var searchQuery = Expression.Lambda<Func<TEntity, (long Id, int Field)>>(memberInitExpression, p);
            var data = await rep.Where(expression).Select(searchQuery).ToListAsync();
            return data;
        }
        /// <summary>
        /// 整个表转为下拉列表，采集string类型字段
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public async Task<List<DropdownDataResult>> GetStringList<TEntity>(string fieldName = "") where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            List<DropdownDataResult> list = await rep.ConvertListSearch(fieldName).ToListAsync();
            return list;
        }

        /// <summary>
        /// 根据表达式过滤数据获取下拉列表，采集string类型字段
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="fieldName"></param>
        /// <param name="isIgnoreTenant"></param>
        /// <returns></returns>
        public async Task<List<DropdownDataResult>> GetStringList<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName = "", bool isIgnoreTenant = false) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            if (isIgnoreTenant)
            {
                List<DropdownDataResult> list = await rep.IgnoreTenantFilter().Where(expression).ConvertListSearch(fieldName).ToListAsync();
                return list;
            }
            else
            {
                List<DropdownDataResult> list = await rep.Where(expression).ConvertListSearch(fieldName).ToListAsync();
                return list;
            }
        }

        /// <summary>
        /// 获取下拉选中列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="fieldName"></param>
        /// <param name="isIgnoreTenant"></param>
        /// <returns></returns>
        public async Task<List<DropdownSelectionResult>> GetCheckList<TEntity>(Expression<Func<TEntity, bool>> expression, IEnumerable<long> checkIds, string checkFieldName, string fieldName = "" ) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            
            return new List<DropdownSelectionResult>();
        }
        #endregion

        #region 获取集合
        /// <summary>
        /// 查询指定long类型字段集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="fieldName">自定义采集id名称</param>
        /// <returns></returns>
        public async Task<List<long>> GetLongFields<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName = "") where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            if (fieldName.IsNullOrEmpty())
            {
                List<long> list = await rep.Where(expression).Select(p => p.Id).ToListAsync();
                return list;
            }
            // 参数表达式，构建P
            ParameterExpression p = Expression.Parameter(typeof(TEntity), "p");
            // 成员表达式，构建 p.id
            MemberExpression memberExpression = Expression.PropertyOrField(p, fieldName);
            // 转换为查询表达式
            var searchQuery = Expression.Lambda<Func<TEntity, long>>(memberExpression, p);
            return await rep.Where(expression).Select(searchQuery).ToListAsync();
        }

        /// <summary>
        /// 获取id集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="fieldName">自定义采集id名称</param>
        /// <returns></returns>
        public async Task<List<long>> Getlds<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName = "") where TEntity : EntityBaseDO
        {
            return await GetLongFields(expression, fieldName);
        }
        #endregion

        #region 查询单条数据
        /// <summary>
        /// 通过主键查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetDataById<TEntity>(long id) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            return await rep.FindAsync(id);
        }

        /// <summary>
        /// 根据条件查找单条记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="isIgnoreTenant"></param>
        /// <returns></returns>
        public async Task<TEntity> GetDataByCondition<TEntity>(Expression<Func<TEntity, bool>> expression, bool isIgnoreTenant = false) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            if (isIgnoreTenant)
            {
                TEntity entity = await rep.IgnoreTenantFilter().FirstOrDefaultAsync(expression);
                return entity;
            }
            else
            {
                TEntity entity = await rep.FirstOrDefaultAsync(expression);
                return entity;
            }
        }
        #endregion

        #region 获取单个字段
        /// <summary>
        /// 查询单条数据的指定列（int类型）
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<int> GetFirstIntTypeField<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            // 参数表达式，构建P
            ParameterExpression p = Expression.Parameter(typeof(TEntity), "p");
            // 成员表达式，构建 p.id
            MemberExpression memberExpression = Expression.PropertyOrField(p, fieldName);
            // 转换为查询表达式
            var searchQuery = Expression.Lambda<Func<TEntity, int>>(memberExpression, p);
            var data = await rep.Where(expression).Select(searchQuery).FirstOrDefaultAsync();
            return data;
        }

        /// <summary>
        /// 查询单条数据的指定列（string类型）
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<string> GetFirstStringTypeField<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            // 参数表达式，构建P
            ParameterExpression p = Expression.Parameter(typeof(TEntity), "p");
            // 成员表达式，构建 p.id
            MemberExpression memberExpression = Expression.PropertyOrField(p, fieldName);
            // 转换为查询表达式
            var searchQuery = Expression.Lambda<Func<TEntity, string>>(memberExpression, p);
            var data = await rep.Where(expression).Select(searchQuery).FirstOrDefaultAsync();
            return data;
        }

        /// <summary>
        /// 根据条件获取数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> GetCount<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            return await rep.Where(expression).CountAsync();
        }
        #endregion

        #region 多条数据
        /// <summary>
        /// 通过主键集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetDataByIds<TEntity>(List<long> ids) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            return await rep.Where(r => ids.Contains(r.Id)).ToListAsync();
        }

        /// <summary>
        /// 根据条件过滤，返回数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="isIgnoreTenant"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetTableList<TEntity>(Expression<Func<TEntity, bool>> expression, bool isIgnoreTenant = false) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            if (isIgnoreTenant)
            {
                return await rep.IgnoreTenantFilter().Where(expression).ToListAsync();

            }
            else
            {
                return await rep.Where(expression).ToListAsync();
            }
        }
        #endregion

        #region 新增 
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync<TEntity>(TEntity data) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            await rep.AddAsync(data);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<bool> BatchAddAsync<TEntity>(List<TEntity> list) where TEntity : EntityBaseDO
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                DbSet<TEntity> rep = dbContext.Set<TEntity>();
                await rep.AddRangeAsync(list);
                await dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("新增失败");
            }
            return true;
        }

        /// <summary>
        /// 批量复制
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TInsertEntity"></typeparam>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<bool> BatchCopyAsync<TEntity, TInsertEntity>(long tenantId = 0) where TInsertEntity : EntityTenantDO where TEntity : EntityBaseDO
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                DbSet<TEntity> soucesRep = dbContext.Set<TEntity>();
                DbSet<TInsertEntity> insertRep = dbContext.Set<TInsertEntity>();
                List<TInsertEntity> list = await soucesRep.Select(p => p.Adapt<TInsertEntity>()).ToListAsync();
                if (!tenantId.Equals(0))
                {
                    list.ForEach(p =>
                    {
                        p.TenantId = tenantId;
                        p.Id = YitIdHelper.NextId();
                    });
                }
                await insertRep.AddRangeAsync(list);
                await dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("复制失败");
            }           
            return true;
        }

        /// <summary>
        /// 批量复制，模型自动转查询条件
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TInsertEntity"></typeparam>
        /// <typeparam name="SearchInput"></typeparam>
        /// <param name="input"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<bool> BatchCopyAsync<TEntity, TInsertEntity, SearchInput>(SearchInput input, long tenantId = 0) where TInsertEntity : EntityTenantDO where TEntity : EntityBaseDO
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                DbSet<TEntity> soucesRep = dbContext.Set<TEntity>();
                DbSet<TInsertEntity> rep = dbContext.Set<TInsertEntity>();
                List<TInsertEntity> list = await soucesRep.AsNoTracking().AddSearchCriteria(input).Select(p => p.Adapt<TInsertEntity>()).ToListAsync();
                if (!tenantId.Equals(0))
                {
                    list.ForEach(p =>
                    {
                        p.TenantId = tenantId;
                        p.Id = YitIdHelper.NextId();
                    });
                }
                await rep.AddRangeAsync(list);
                await dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("复制失败");
            }           
            return true;
        }

        /// <summary>
        /// 批量复制，带有查询条件
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TInsertEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<bool> BatchCopyAsync<TSource, TInsertEntity>(Expression<Func<TSource, bool>> expression, long tenantId = 0) where TInsertEntity : EntityTenantDO where TSource : EntityBaseDO
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                DbSet<TSource> soucesRep = dbContext.Set<TSource>();
                DbSet<TInsertEntity> rep = dbContext.Set<TInsertEntity>();
                List<TInsertEntity> list = await soucesRep.AsNoTracking().Where(expression).Select(p => p.Adapt<TInsertEntity>()).ToListAsync();
                if (!tenantId.Equals(0))
                {
                    list.ForEach(p => { p.TenantId = tenantId; p.Id = YitIdHelper.NextId(); });
                }
                await rep.AddRangeAsync(list);
                await dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("复制失败");
            }          
            return true;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync<TEntity>(TEntity data) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            rep.Update(data);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<bool> BatchUpdateAsync<TEntity>(List<TEntity> list) where TEntity : EntityBaseDO
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                DbSet<TEntity> rep = dbContext.Set<TEntity>();
                rep.UpdateRange(list);
                await dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("更新失败");
            }
            return true;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 根据主键删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync<TEntity>(long id) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            TEntity data = await rep.FindAsync(id);
            rep.Remove(data);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 根据条件删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            TEntity data = await rep.Where(expression).FirstOrDefaultAsync();
            rep.Remove(data);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 根据主键假删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public async Task<bool> FakeDeleteAsync<TEntity>(long id) where TEntity : EntityBaseDO
        {
            DbSet<TEntity> rep = dbContext.Set<TEntity>();
            TEntity data = await rep.FirstOrDefaultAsync(p => p.Id == id);
            data.IsDeleted = true;
            rep.Update(data);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 批量删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<bool> BatchDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                DbSet<TEntity> rep = dbContext.Set<TEntity>();
                rep.RemoveRange(rep.Where(expression));
                await dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("删除失败");
            }       
            return true;
        }
        #endregion
    }
}