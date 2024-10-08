using IDataSphere.Interfaces.BackEnd;
using Mapster;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.AuditTypeManage;
using Model.Repositotys.BasicData;

namespace Service.BackEnd.AuditTypeManage
{
    /// <summary>
    /// 审核角色类型配置表业务类
    /// </summary>
    public class AuditTypeManageServiceImpl : IAuditTypeManageService
    {
        #region 构造函数
        private readonly IAuditTypeManageDao _auditTypeManageDao;
        public AuditTypeManageServiceImpl(IAuditTypeManageDao auditTypeManageDao)
        {
            this._auditTypeManageDao = auditTypeManageDao;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult> GetAuditTypePage(GetAuditTypePageInput input)
        {
            return await _auditTypeManageDao.GetAuditTypePage(input);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddAuditType(AddAuditTypeInput input)
        {          
            T_AuditType data = input.Adapt<T_AuditType>();
            return await _auditTypeManageDao.AddAsync(data);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAuditType(UpdateAuditTypeInput input) 
        {
            T_AuditType data = input.Adapt<T_AuditType>();
            data.Id = input.Id;
            return await _auditTypeManageDao.UpdateAsync(data);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAuditType(long id)
        {        
            return await _auditTypeManageDao.DeleteAsync<T_AuditType>(p => p.Id == id);
        }
        #endregion
    }
}
