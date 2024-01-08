using IDataSphere.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.Repositotys;
using System.Linq.Expressions;

namespace IDataSphere.Interfaces
{
    /// <summary>
    /// 数据库操作CURD方法
    /// </summary>
    public interface IBaseDao
    {

        #region 构造函数       

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        public SqlDbContext GetDBContext();


        /// <summary>
        /// 用户Id
        /// </summary>
        /// <returns></returns>
        public long UserId();

        /// <summary>
        /// 租户Id
        /// </summary>
        /// <returns></returns>
        public long TenantId();

        /// <summary>
        /// 获取指定Rep
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="isIgnoreTenant"></param>
        /// <returns></returns>
        public DbSet<TEntity> GetRep<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : EntityBaseDO;
        #endregion

        #region 判断
        /// <summary>
        /// 根据条件判断数据是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DataExisted<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : EntityBaseDO;

        /// <summary>
        /// 判断Id是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> IdExisted<TEntity>(long id) where TEntity : EntityBaseDO;
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
        public Task<PaginationResultModel> GetPage<TEntity, Input>(Input input, bool isIgnoreTenant = false)
            where TEntity : EntityBaseDO
            where Input : PageInput;

        /// <summary>
        /// 查询语句自动转为分页
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public Task<PaginationResultModel> AdaptPage<TEntity>(IQueryable<TEntity> query, int pageSize, int pageNo);
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
        public Task<List<(long Id, int Field)>> GetIntList<TEntity, Int>(Expression<Func<TEntity, bool>> expression, string fieldName)
            where TEntity : EntityBaseDO;
        /// <summary>
        /// 整个表转为下拉列表，采集string类型字段
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public Task<List<DropdownDataModel>> GetStringList<TEntity>(string fieldName = "") where TEntity : EntityBaseDO;
        /// <summary>
        /// 根据表达式过滤数据获取下拉列表，采集string类型字段
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="fieldName"></param>
        /// <param name="isIgnoreTenant"></param>
        /// <returns></returns>
        public Task<List<DropdownDataModel>> GetStringList<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName = "", bool isIgnoreTenant = false)
            where TEntity : EntityBaseDO;
        #endregion

        #region 获取集合
        /// <summary>
        /// 查询指定long类型字段集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="fieldName">自定义采集id名称</param>
        /// <returns></returns>
        public Task<List<long>> GetLongFields<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName = "")
            where TEntity : EntityBaseDO;
        #endregion

        #region 查询单条数据
        /// <summary>
        /// 通过主键查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TEntity> GetDataById<TEntity>(long id) where TEntity : EntityBaseDO;

        /// <summary>
        /// 根据条件查找单条记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="isIgnoreTenant"></param>
        /// <returns></returns>
        public Task<TEntity> GetDataByCondition<TEntity>(Expression<Func<TEntity, bool>> expression, bool isIgnoreTenant = false)
            where TEntity : EntityBaseDO;
        #endregion

        #region 获取单个字段
        /// <summary>
        /// 查询单条数据的指定列（int类型）
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<int> GetFirstIntTypeField<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName)
            where TEntity : EntityBaseDO;

        /// <summary>
        /// 查询单条数据的指定列（string类型）
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<string> GetFirstStringTypeField<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName)
            where TEntity : EntityBaseDO;

        /// <summary>
        /// 根据条件获取数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> GetCount<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO;
        #endregion

        #region 多条数据
        /// <summary>
        /// 通过主键集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<List<TEntity>> GetDataByIds<TEntity>(List<long> ids) where TEntity : EntityBaseDO;

        /// <summary>
        /// 根据条件过滤，返回数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="isIgnoreTenant"></param>
        /// <returns></returns>
        public Task<List<TEntity>> GetTableList<TEntity>(Expression<Func<TEntity, bool>> expression, bool isIgnoreTenant = false)
            where TEntity : EntityBaseDO;
        #endregion

        #region 新增 
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<bool> AddAsync<TEntity>(TEntity data) where TEntity : EntityBaseDO;

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Task<bool> BatchAddAsync<TEntity>(List<TEntity> list) where TEntity : EntityBaseDO;

        /// <summary>
        /// 批量复制
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TInsertEntity"></typeparam>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Task<bool> BatchCopyAsync<TEntity, TInsertEntity>(long tenantId = 0)
            where TInsertEntity : EntityTenantDO
            where TEntity : EntityBaseDO;

        /// <summary>
        /// 批量复制，模型自动转查询条件
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TInsertEntity"></typeparam>
        /// <typeparam name="SearchInput"></typeparam>
        /// <param name="input"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Task<bool> BatchCopyAsync<TEntity, TInsertEntity, SearchInput>(SearchInput input, long tenantId = 0)
            where TInsertEntity : EntityTenantDO
            where TEntity : EntityBaseDO;

        /// <summary>
        /// 批量复制，带有查询条件
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TInsertEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Task<bool> BatchCopyAsync<TSource, TInsertEntity>(Expression<Func<TSource, bool>> expression, long tenantId = 0)
            where TInsertEntity : EntityTenantDO
            where TSource : EntityBaseDO;
        #endregion

        #region 更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<bool> UpdateAsync<TEntity>(TEntity data) where TEntity : EntityBaseDO;
        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public Task<bool> BatchUpdateAsync<TEntity>(List<TEntity> list) where TEntity : EntityBaseDO;
        #endregion

        #region 删除
        /// <summary>
        /// 根据主键删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public Task<bool> DeleteAsync<TEntity>(long id) where TEntity : EntityBaseDO;

        /// <summary>
        /// 根据条件删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : EntityBaseDO;

        /// <summary>
        /// 根据主键假删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public Task<bool> FakeDeleteAsync<TEntity>(long id) where TEntity : EntityBaseDO;

        /// <summary>
        /// 批量删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<bool> BatchDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : EntityBaseDO;
        #endregion
    }
}
