using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.AuditNodeConfigManage;
using IDataSphere.Interface.BackEnd.AuditNodeConfigManage;
namespace Service.BackEnd.AuditNodeConfigManage
{
    /// <summary>
    /// 审核流程配置表业务接口
    /// </summary>
    public interface IAuditNodeConfigManageService
    {     
        Task<bool> UpdateAuditNodeConfig(UpdateAuditNodeConfigInput input);
        Task<bool> DeleteAuditNodeConfig(long id);
        Task<PageResult> GetAuditNodeConfigPage(GetAuditNodeConfigPageInput input);
        Task<bool> AddAuditNodeConfig(AddAuditNodeConfigInput input);
    }
}
