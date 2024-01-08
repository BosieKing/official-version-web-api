using Model.Commons.CoreData;
using Model.DTOs.BackEnd.TenantMenuManage;

namespace IDataSphere.Interfaces.BackEnd
{
    /// <summary>
    /// 后台租户菜单管理数据访问接口
    /// </summary>
    /// <remarks>T_TenantMenu</remarks>
    public interface ITenantMenuManageDao : IBaseDao
    {
        Task<(int count, List<MenuTreeModel> menuListInfo, List<MenuTreeModel> buttonListInfo)> GetTenantMenuPage(GetTenantMenuPageInput input);
    }
}
