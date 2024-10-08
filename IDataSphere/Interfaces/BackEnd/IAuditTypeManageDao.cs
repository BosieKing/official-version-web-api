using Model.Commons.Domain;
using Model.DTOs.BackEnd.AuditTypeManage;
using Model.Repositotys.BasicData;

namespace IDataSphere.Interfaces.BackEnd
{
    /// <summary>
    /// 审核角色类型配置表数据访问接口
    /// </summary>
    public interface IAuditTypeManageDao : IBaseDao<T_AuditType>
    {
        Task<PageResult> GetAuditTypePage(GetAuditTypePageInput input);
    }
}
