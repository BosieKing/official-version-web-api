using Model.Repositotys.BasicData;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using IDataSphere.Interfaces;
using IDataSphere.Extensions;
using Model.DTOs.BackEnd.AuditTypeManage;
namespace IDataSphere.Interface.BackEnd.AuditTypeManage
{
     /// <summary>
    /// 审核角色类型配置表数据访问接口
    /// </summary>
    public interface IAuditTypeManageDao: IBaseDao<T_AuditType>
    {        
       Task<PageResult> GetAuditTypePage(GetAuditTypePageInput input);
    }
}
