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
    public interface IBaseDao<TEntity>
    {
        Task<PageResult> AdaptPage<TEntity>(IQueryable<TEntity> query, int pageSize, int pageNo);
        Task<bool> AddAsync<TEntity>(TEntity data) where TEntity : EntityBaseDO;
        Task<bool> BatchAddAsync<TEntity>(List<TEntity> list) where TEntity : EntityBaseDO;
        Task<bool> BatchDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO;
        Task<bool> BatchUpdateAsync<TEntity>(List<TEntity> list) where TEntity : EntityBaseDO;
        Task<bool> DataExisted<TEntity>(Expression<Func<TEntity, bool>> expression, bool isIgnoreTenant = false) where TEntity : EntityBaseDO;
        Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO;
        Task<bool> DeleteAsync<TEntity>(long id) where TEntity : EntityBaseDO;
        Task<bool> FakeDeleteAsync<TEntity>(long id) where TEntity : EntityBaseDO;
        Task<List<DropdownSelectionResult>> GetCheckList<TEntity>(string checkFieldName, List<long> checkIds, Expression<Func<TEntity, bool>> expression = null, string fieldName = "") where TEntity : EntityBaseDO;
        Task<int> GetCount<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO;
        Task<TEntity> GetDataByCondition<TEntity>(Expression<Func<TEntity, bool>> expression, bool isIgnoreTenant = false) where TEntity : EntityBaseDO;
        Task<TEntity> GetDataById<TEntity>(long id) where TEntity : EntityBaseDO;
        Task<List<TEntity>> GetDataByIds<TEntity>(List<long> ids) where TEntity : EntityBaseDO;
        SqlDbContext GetDBContext();
        Task<int> GetFirstIntTypeField<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName) where TEntity : EntityBaseDO;
        Task<string> GetFirstStringTypeField<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName) where TEntity : EntityBaseDO;
        Task<List<long>> Getlds<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : EntityBaseDO;
        Task<List<long>> GetLongArray<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName, bool isIgnoreTenant = false) where TEntity : EntityBaseDO;
        Task<PageResult> GetPage<TEntity, Input>(Input input, bool isIgnoreTenant = false)
            where TEntity : EntityBaseDO
            where Input : PageInput;
        Task<List<DropdownResult>> GetDropdownResultList<TEntity>(Expression<Func<TEntity, bool>> expression, string fieldName, bool isIgnoreTenant = false) where TEntity : EntityBaseDO;
        Task<List<TEntity>> GetTableList<TEntity>(Expression<Func<TEntity, bool>> expression, bool isIgnoreTenant = false) where TEntity : EntityBaseDO;
        Task<bool> IdExisted<TEntity>(long id) where TEntity : EntityBaseDO;
        long TenantId();
        Task<bool> UpdateAsync<TEntity>(TEntity data) where TEntity : EntityBaseDO;
        long UserId();
    }
}
