using Model.Repositotys.BasicData;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using IDataSphere.Interfaces;
using IDataSphere.Extensions;
using Model.DTOs.BackEnd.AuditNodeConfigManage;
namespace IDataSphere.Interface.BackEnd.AuditNodeConfigManage
{
     /// <summary>
    /// 审核流程配置表数据访问接口
    /// </summary>
    public interface IAuditNodeConfigManageDao: IBaseDao<T_AuditNodeConfig>
    {        
       Task<PageResult> GetAuditNodeConfigPage(GetAuditNodeConfigPageInput input);
    }
}
