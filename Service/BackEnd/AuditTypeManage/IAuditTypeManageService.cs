using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.AuditTypeManage;
using IDataSphere.Interface.BackEnd.AuditTypeManage;
namespace Service.BackEnd.AuditTypeManage
{
    /// <summary>
    /// 审核角色类型配置表业务接口
    /// </summary>
    public interface IAuditTypeManageService
    {     
        Task<bool> UpdateAuditType(UpdateAuditTypeInput input);
        Task<bool> DeleteAuditType(long id);
        Task<PageResult> GetAuditTypePage(GetAuditTypePageInput input);
        Task<bool> AddAuditType(AddAuditTypeInput input);
    }
}
