using IDataSphere.Repositoty;
using SharedLibrary.Models.DomainModels;
using SharedLibrary.Models.SharedDataModels;
using System.Linq.Expressions;

namespace IDataSphere.Interface
{
    /// <summary>
    /// 数据库操作CURD方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseDao<T>
    {
        Task<PaginationResultModel> AdaptPage<T_Souce>(IQueryable<T_Souce> query, int pageSize, int pageNo);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<bool> AddAsync(T data);

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<bool> AddAsync<TEntity>(TEntity data) where TEntity : EntityBaseDO;


        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> BatchAddAsync(List<T> list);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> BatchAddAsync<TEntity>(List<TEntity> list) where TEntity : EntityBaseDO;

        /// <summary>
        /// 批量复制到新表
        /// </summary>
        /// <typeparam name="TSource">数据源表</typeparam>
        /// <typeparam name="TInsertEntity">需要插入的表</typeparam>
        /// <param name="tenantId">租户id，如果不为0则覆盖采集的添加人的租户id</param>
        /// <returns></returns>
        Task<bool> BatchCopyAsync<TSource, TInsertEntity>(long tenantId = 0) where TInsertEntity : EntityTenantDO where TSource : EntityBaseDO;

        /// <summary>
        /// 批量复制，带有过滤条件
        /// </summary>
        /// <typeparam name="TSource">数据源</typeparam>
        /// <typeparam name="TInsertEntity">需要插入的表</typeparam>
        /// <typeparam name="SearchInput">对数据源过滤的条件</typeparam>
        /// <param name="input">对数据源过滤的条件</param>
        /// <param name="tenantId">租户id，如果不为0则覆盖采集的添加人的租户id</param>
        /// <returns></returns>
        Task<bool> BatchCopyAsync<TSource, TInsertEntity, SearchInput>(SearchInput input, long tenantId = 0)
            where TSource : EntityBaseDO
            where TInsertEntity : EntityTenantDO;

        /// <summary>
        /// 批量复制,带有过滤条件
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TInsertEntity"></typeparam>
        /// <param name="expression">Where条件表达式</param>
        /// <param name="tenantId">租户id，如果不为0则覆盖采集的添加人的租户id</param>
        /// <returns></returns>
        Task<bool> BatchCopyAsync<TSource, TInsertEntity>(Expression<Func<TSource, bool>> expression, long tenantId = 0)
            where TSource : EntityBaseDO
            where TInsertEntity : EntityTenantDO;

        Task<bool> BatchDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO;
        Task<bool> DataExisted<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO;
        Task<bool> DataExisted(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="id">数据源主键id</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="id">数据源主键id</param>
        /// <returns></returns>
        Task<bool> DeleteAsync<TEntity>(long id) where TEntity : EntityBaseDO;

        /// <summary>
        /// 假删除数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id">数据源主键id</param>
        /// <returns></returns>
        Task<bool> FakeDeleteAsync<TEntity>(long id) where TEntity : EntityBaseDO;

        /// <summary>
        /// 根据Id获取单条数据
        /// </summary>
        /// <param name="id">数据源主键id</param>
        /// <returns></returns>
        Task<T> GetDataById(long id);

        /// <summary>
        /// 单表查询下拉列表
        /// </summary>
        /// <param name="fieldName">自定义采集字段名称</param>
        /// <returns></returns>
        Task<List<DropdownDataModel>> GetList(string fieldName = "");

        /// <summary>
        /// 单表查询下拉列表
        /// </summary>
        /// <typeparam name="TEntity">自定义采集字段名称</typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        Task<List<DropdownDataModel>> GetList<TEntity>(string fieldName = "") where TEntity : EntityBaseDO;


        /// <summary>
        /// 单表查询下拉列表
        /// </summary>
        /// <typeparam name="TEntity">自定义采集字段名称</typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        //Task<List<ListResult>> GetCheckList<TEntity, TSouce>(long souceId, string fieldName = "") where TEntity  : EntityBaseDO;
        /// <summary>
        /// 单表查询下拉列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression">表达式</param>
        /// <param name="fieldName">自定义采集字段</param>
        /// <param name="isIgnoreTenant">是否忽略租户过滤查询器，默认不忽略</param>
        /// <returns></returns>
        Task<List<DropdownDataModel>> GetList<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName = "", bool isIgnoreTenant = false)
            where TEntity : EntityBaseDO;

        /// <summary>
        /// 单表查询下拉列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">表达式</param>
        /// <param name="fieldName">自定义采集字段</param>
        /// <param name="isIgnoreTenant">是否忽略租户过滤查询器，默认不忽略</param>
        /// <returns></returns>
        Task<List<DropdownDataModel>> GetList(Expression<Func<T, bool>> expression, string fieldName = "", bool isIgnoreTenant = false);

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="SearchInput"></typeparam>
        /// <param name="input">查询模型</param>
        /// <param name="isIgnoreTenant">是否忽略租户过滤查询器，默认不忽略</param>
        /// <returns></returns>
        Task<PaginationResultModel> GetPage<SearchInput>(SearchInput input, bool isIgnoreTenant = false) where SearchInput : PageInput;

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="SearchInput"></typeparam>
        /// <param name="input">查询模型</param>
        /// <param name="isIgnoreTenant">是否忽略租户过滤查询器，默认不忽略</param>
        /// <returns></returns>
        Task<PaginationResultModel> GetPage<TEntity, SearchInput>(SearchInput input, bool isIgnoreTenant = false)
            where TEntity : EntityBaseDO
            where SearchInput : PageInput;

        /// <summary>
        /// 判断id是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IdExisted(long id);


        /// <summary>
        /// 判断id是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IdExisted<TEntity>(long id) where TEntity : EntityBaseDO;


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T data);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync<TEntity>(TEntity data) where TEntity : EntityBaseDO;
        Task<bool> UpdateByFieldAsync<TEntity, TInputEntity>(long Key, TInputEntity input, params string[] fileld) where TEntity : EntityBaseDO;
    }
}
