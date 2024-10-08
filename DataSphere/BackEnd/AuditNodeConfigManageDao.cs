using Model.Repositotys.BasicData;
using Microsoft.EntityFrameworkCore;
using UtilityToolkit.Utils;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.AuditNodeConfigManage;
using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.BackEnd;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 审核流程配置表数据访问层
    /// </summary>
    public class AuditNodeConfigManageDao : BaseDao<T_AuditNodeConfig>, IAuditNodeConfigManageDao
    {
      
        #region 构造函数
        public AuditNodeConfigManageDao(SqlDbContext dbContext) : base(dbContext)
        {

        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> GetAuditNodeConfigPage(GetAuditNodeConfigPageInput input) 
        {

            var query = dbContext.AuditNodeConfigRep.Where(p => p.AuditNodeConfigType == input.AuditNodeConfigType)
                                 .GroupJoin(dbContext.AuditNodeConfigOptionRep, c => c.Id, co => co.AuditNodeConfigId, (c, co) => new
                                 {
                                     c.Name,
                                     c.NodeLevel,
                                     OptionList = co.Select(p => new
                                     {
                                         Id = p.Id,
                                         AuditLevel = p.AuditLevel,
                                         AuditType = p.AuditType,
                                         AuditUserCount = p.AuditUserCount,
                                         ApproveStrategy = p.ApproveStrategy,
                                         FailRetrunLevel = p.FailRetrunLevel,
                                     }).OrderBy(o => o.AuditLevel)
                                 }).OrderBy(p => p.NodeLevel).ToList();          
            return query;
        }
        #endregion

        #region 新增

        #endregion

        #region 更新

        #endregion

        #region 删除

        #endregion
    }
}
