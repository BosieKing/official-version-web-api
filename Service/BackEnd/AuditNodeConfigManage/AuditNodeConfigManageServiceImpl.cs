using IDataSphere.Interface.BackEnd;
using Model.Repositotys.BasicData;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.AuditNodeConfigManage;
using IDataSphere.Interface.BackEnd.AuditNodeConfigManage;
using System.Text;
using UtilityToolkit.Utils;

namespace Service.BackEnd.AuditNodeConfigManage
{
    /// <summary>
    /// 审核流程配置表业务类
    /// </summary>
    public class AuditNodeConfigManageServiceImpl : IAuditNodeConfigManageService
    {
        #region 构造函数
        private readonly IAuditNodeConfigManageDao _auditNodeConfigManageDao;
        public AuditNodeConfigManageServiceImpl(IAuditNodeConfigManageDao auditNodeConfigManageDao)
        {
            this._auditNodeConfigManageDao = auditNodeConfigManageDao;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult> GetAuditNodeConfigPage(GetAuditNodeConfigPageInput input)
        {
            return await _auditNodeConfigManageDao.GetAuditNodeConfigPage(input);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddAuditNodeConfig(AddAuditNodeConfigInput input)
        {
            T_AuditNodeConfig data = input.Adapt<T_AuditNodeConfig>();
            return await _auditNodeConfigManageDao.AddAsync(data);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAuditNodeConfig(UpdateAuditNodeConfigInput input) 
        {
            T_AuditNodeConfig data = input.Adapt<T_AuditNodeConfig>();
            data.Id = input.Id;
            return await _auditNodeConfigManageDao.UpdateAsync(data);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAuditNodeConfig(long id)
        {        
            return await _auditNodeConfigManageDao.DeleteAsync(p => p.Id == id);
        }
        #endregion
    }
}
