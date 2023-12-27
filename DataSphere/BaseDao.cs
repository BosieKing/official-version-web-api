using IDataSphere.Extensions;
using IDataSphere.Interface;
using IDataSphere.Repositoty;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models.DomainModels;
using SharedLibrary.Models.SharedDataModels;
using System.Linq.Expressions;
using Yitter.IdGenerator;

namespace DataSphere
{
    /// <summary>
    /// 常规的操作数据库方法
    /// </summary>
    public class BaseDao<T> : IBaseDao<T> where T : EntityBaseDO
    {
        #region 构造函数   
        public readonly SqlDbContext dMDbContext;
        public BaseDao(SqlDbContext dMDbContext)
        {
            this.dMDbContext = dMDbContext;
        }

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        public DbContextAbstract GetDBContext()
        {
            return dMDbContext;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 通过id查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> TEstID()
        {
            var rep = dMDbContext.Set<T>();
            return await rep.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 根据条件判断数据是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DataExisted<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO
        {
            var rep = dMDbContext.Set<TEntity>();
            return await rep.AnyAsync(expression);
        }

        /// <summary>
        /// 根据条件判断数据是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DataExisted(Expression<Func<T, bool>> expression)
        {
            var rep = dMDbContext.Set<T>();
            return await rep.AnyAsync(expression);
        }
        /// <summary>
        /// 判断Id是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IdExisted(long id)
        {
            var rep = dMDbContext.Set<T>();
            return await rep.AnyAsync(p => p.Id == id);
        }

        /// <summary>
        /// 判断Id是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IdExisted<TEntity>(long id) where TEntity : EntityBaseDO
        {
            var rep = dMDbContext.Set<TEntity>();
            return await rep.AnyAsync(p => p.Id == id);
        }

        /// <summary>
        /// 通过id查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetDataById(long id)
        {
            var rep = dMDbContext.Set<T>();
            return await rep.FindAsync(id);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="Input"></typeparam>
        /// <param name="input"></param>
        /// <param name="isIgnoreTenant">是否忽略租户</param>
        /// <returns></returns>
        public async Task<PaginationResultModel> GetPage<Input>(Input input, bool isIgnoreTenant = false) where Input : PageInput
        {
            var rep = dMDbContext.Set<T>();
            var query = rep.AddSearchCriteria(input);
            if (isIgnoreTenant)
            {
                var count = await query.IgnoreTenantFilter().CountAsync();
                var data = await query.IgnoreTenantFilter().Select(p => p).Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize).ToListAsync();
                return new PaginationResultModel(input.PageNo, input.PageSize, count, data);
            }
            else
            {
                var count = await query.CountAsync();
                var data = await query.Select(p => p).Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize).ToListAsync();
                return new PaginationResultModel(input.PageNo, input.PageSize, count, data);
            }
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="Input"></typeparam>
        /// <param name="input"></param>
        /// <param name="isIgnoreTenant">是否忽略租户</param>
        /// <returns></returns>
        public async Task<PaginationResultModel> AdaptPage<T_Souce>(IQueryable<T_Souce> query, int pageSize, int pageNo)
        {
            var count = await query.CountAsync();
            var data = await query.Select(p => p).Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginationResultModel(pageNo, pageSize, count, data);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="Input"></typeparam>
        /// <param name="input"></param>
        /// <param name="isIgnoreTenant">是否忽略租户</param>
        /// <returns></returns>
        public async Task<PaginationResultModel> GetPage<TEntity, Input>(Input input, bool isIgnoreTenant = false) where TEntity : EntityBaseDO where Input : PageInput
        {
            var rep = dMDbContext.Set<TEntity>();
            var query = rep.AddSearchCriteria(input);
            if (isIgnoreTenant)
            {
                var count = await query.IgnoreTenantFilter().CountAsync();
                var data = await query.IgnoreTenantFilter().Select(p => p).Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize).ToListAsync();
                return new PaginationResultModel(input.PageNo, input.PageSize, count, data);
            }
            else
            {
                var count = await query.CountAsync();
                var data = await query.Select(p => p).Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize).ToListAsync();
                return new PaginationResultModel(input.PageNo, input.PageSize, count, data);
            }
        }

        /// <summary>
        /// 单表查询下拉列表
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public async Task<List<DropdownDataModel>> GetList(string fieldName = "")
        {
            var rep = dMDbContext.Set<T>();
            var list = await rep.ConvertListSearch(fieldName).ToListAsync();
            return list;
        }

        /// <summary>
        /// 单表查询下拉列表
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public async Task<List<DropdownDataModel>> GetList<TEntity>(string fieldName = "") where TEntity : EntityBaseDO
        {
            var rep = dMDbContext.Set<TEntity>();
            var list = await rep.ConvertListSearch(fieldName).ToListAsync();
            return list;
        }

        /// <summary>
        /// 单表查询下拉列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression">表达式</param>
        /// <param name="fieldName">自定义采集字段</param>
        /// <param name="isIgnoreTenant">是否忽略租户过滤查询器，默认不忽略</param>
        /// <returns></returns>
        public async Task<List<DropdownDataModel>> GetList<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName = "", bool isIgnoreTenant = false) where TEntity : EntityBaseDO
        {
            var rep = dMDbContext.Set<TEntity>();
            if (isIgnoreTenant)
            {
                var list = await rep.IgnoreTenantFilter().Where(p => !p.IsDeleted).Where(expression).ConvertListSearch(fieldName).ToListAsync();
                return list;
            }
            else
            {
                var list = await rep.Where(expression).ConvertListSearch(fieldName).ToListAsync();
                return list;
            }
        }

        /// <summary>
        /// 单表查询下拉列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">表达式</param>
        /// <param name="fieldName">自定义采集字段</param>
        /// <param name="isIgnoreTenant">是否忽略租户过滤查询器，默认不忽略</param>
        /// <returns></returns>
        public async Task<List<DropdownDataModel>> GetList(Expression<Func<T, bool>> expression, string fieldName = "", bool isIgnoreTenant = false)
        {
            var rep = dMDbContext.Set<T>();
            if (isIgnoreTenant)
            {
                var list = await rep.IgnoreTenantFilter().Where(expression).ConvertListSearch(fieldName).ToListAsync();
                return list;
            }
            else
            {
                var list = await rep.Where(expression).ConvertListSearch(fieldName).ToListAsync();
                return list;
            }
        }


        /// 单表查询下拉列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression">表达式</param>
        /// <param name="fieldName">自定义采集字段</param>
        /// <param name="isIgnoreTenant">是否忽略租户过滤查询器，默认不忽略</param>
        /// <returns></returns>
        //public async Task<List<ListResult>> GetCheckList<TEntity, TSouce>(string foreignKey, string fieldName = "") where TEntity : EntityBaseDO where TSouce : EntityBaseDO
        //{
        //    var rep = dMDbContext.Set<TEntity>();
        //    var souceRep = dMDbContext.Set<TSouce>();
        //    var p = Expression.Parameter(typeof(TSouce), "s");
        //    MemberExpression idMemberExpression = Expression.PropertyOrField(p, foreignKey);
        //    MemberAssignment idMemberAssignment = Expression.Bind(typeof(long), idMemberExpression);
        //    NewExpression result = Expression.New(typeof(long));
        //    MemberInitExpression memberInitExpression = Expression.MemberInit(result, idMemberAssignment);
        //    var searchQuery = Expression.Lambda<Func<TSouce,long>>(memberInitExpression, p);
        //    var checkIds = souceRep.Select(searchQuery);
        //    var list = await rep.ConvertListSearch(fieldName).ToListAsync();


        //}
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(T data)
        {
            var rep = dMDbContext.Set<T>();
            await rep.AddAsync(data);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync<TEntity>(TEntity data) where TEntity : EntityBaseDO
        {
            var rep = dMDbContext.Set<TEntity>();
            await rep.AddAsync(data);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<bool> BatchAddAsync(List<T> list)
        {
            var rep = dMDbContext.Set<T>();
            await rep.AddRangeAsync(list);
            await dMDbContext.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<bool> BatchAddAsync<TEntity>(List<TEntity> list) where TEntity : EntityBaseDO
        {
            var rep = dMDbContext.Set<TEntity>();
            await rep.AddRangeAsync(list);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 通过数据源批量复制
        /// </summary>
        /// <typeparam name="TSource">数据源</typeparam>
        /// <typeparam name="TInsertEntity">需要插入的表</typeparam>
        /// <param name="souces">数据源查询语句</param>
        /// <returns></returns>
        public async Task<bool> BatchCopyAsync<TSource, TInsertEntity>(long tenantId = 0) where TInsertEntity : EntityTenantDO where TSource : EntityBaseDO
        {
            var soucesRep = dMDbContext.Set<TSource>();
            var insertRep = dMDbContext.Set<TInsertEntity>();
            var list = await soucesRep.AsNoTracking().Select(p => p.Adapt<TInsertEntity>()).ToListAsync();
            if (!tenantId.Equals(0))
            {
                list.ForEach(p =>
                {
                    p.TenantId = tenantId;
                    p.Id = YitIdHelper.NextId();
                });
            }
            await insertRep.AddRangeAsync(list);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 通过数据源批量复制，带有查询条件
        /// </summary>
        /// <typeparam name="TSource">数据源</typeparam>
        /// <typeparam name="TInsertEntity">需要插入的表</typeparam>
        /// <typeparam name="SearchInput">对数据源过滤的条件</typeparam>
        /// <param name="input">对数据源过滤的条件</param>
        /// <param name="tenantId">租户id，不为0</param>
        /// <returns></returns>
        public async Task<bool> BatchCopyAsync<TSource, TInsertEntity, SearchInput>(SearchInput input, long tenantId = 0) where TInsertEntity : EntityTenantDO where TSource : EntityBaseDO
        {
            var soucesRep = dMDbContext.Set<TSource>();
            var rep = dMDbContext.Set<TInsertEntity>();
            var list = await soucesRep.AsNoTracking().AddSearchCriteria(input).Select(p => p.Adapt<TInsertEntity>()).ToListAsync();
            if (!tenantId.Equals(0))
            {
                list.ForEach(p =>
                {
                    p.TenantId = tenantId;
                    p.Id = YitIdHelper.NextId();
                });
            }
            await rep.AddRangeAsync(list);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 批量复制
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TInsertEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<bool> BatchCopyAsync<TSource, TInsertEntity>(Expression<Func<TSource, bool>> expression, long tenantId = 0) where TInsertEntity : EntityTenantDO where TSource : EntityBaseDO
        {
            var soucesRep = dMDbContext.Set<TSource>();
            var rep = dMDbContext.Set<TInsertEntity>();
            var list = await soucesRep.AsNoTracking().Where(expression).Select(p => p.Adapt<TInsertEntity>()).ToListAsync();
            if (!tenantId.Equals(0))
            {
                list.ForEach(p => { p.TenantId = tenantId; p.Id = YitIdHelper.NextId(); });
            }
            await rep.AddRangeAsync(list);
            await dMDbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T data)
        {
            var rep = dMDbContext.Set<T>();
            rep.Update(data);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync<TEntity>(TEntity data) where TEntity : EntityBaseDO
        {
            var rep = dMDbContext.Set<TEntity>();
            rep.Update(data);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateByFieldAsync<TEntity, TInputEntity>(long Key, TInputEntity input, params string[] fileld) where TEntity : EntityBaseDO
        {
            var rep = dMDbContext.Set<TEntity>();
            var oldData = await rep.IgnoreTenantFilter().FirstOrDefaultAsync(p => p.Id == Key);
            var a = Expression.Parameter(oldData.GetType(), "p");
            var b = Expression.Constant(input);
            List<BinaryExpression> updateFiled = new List<BinaryExpression>();
            foreach (var item in fileld)
            {
                MemberExpression left = Expression.PropertyOrField(a, item);
                MemberExpression right = Expression.PropertyOrField(b, item);
                var assignExpr = Expression.Assign(left, right);
                updateFiled.Add(assignExpr);

            }
            Expression blockExpr = Expression.Block(new ParameterExpression[] { a }, updateFiled);
            //var re = Expression.Convert(blockExpr, typeof(TEntity));
            //var c = Expression.Lambda<Func<TEntity>>(re).Compile().Invoke();
            var sdfs = Expression.Lambda<Func<object>>(blockExpr).Compile().Invoke();
            //var re = Expression.Convert(Expression.Lambda<Func<object>>(blockExpr), typeof(TEntity));
            rep.Update(oldData);
            await dMDbContext.SaveChangesAsync();
            return true;
        }


        #endregion

        #region 删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var rep = dMDbContext.Set<T>();
            var data = await rep.FindAsync(id);
            rep.Remove(data);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync<TEntity>(long id) where TEntity : EntityBaseDO
        {
            var rep = dMDbContext.Set<TEntity>();
            var data = await rep.FindAsync(id);
            rep.Remove(data);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 假删除一条数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> FakeDeleteAsync<TEntity>(long id) where TEntity : EntityBaseDO
        {
            var rep = dMDbContext.Set<TEntity>();
            var data = await rep.FirstOrDefaultAsync(p => p.Id == id);
            data.IsDeleted = true;
            rep.Update(data);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 批量删除一条数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> BatchDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO
        {
            var rep = dMDbContext.Set<TEntity>();
            rep.RemoveRange(rep.Where(expression));
            await dMDbContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}