using Model.Commons.Domain;
using Model.DTOs.BackEnd.AuditNodeConfigManage;
using Model.Repositotys.BasicData;

namespace IDataSphere.Interfaces.BackEnd
{
    /// <summary>
    /// 审核流程配置表数据访问接口
    /// </summary>
    public interface IAuditNodeConfigManageDao : IBaseDao<T_AuditNodeConfig>
    {
        Task<dynamic> GetAuditNodeConfigPage(GetAuditNodeConfigPageInput input);
    }
}
