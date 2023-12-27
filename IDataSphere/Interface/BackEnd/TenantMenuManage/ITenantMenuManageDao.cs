using IDataSphere.Repositoty;
using SharedLibrary.Models.CoreDataModels;

namespace IDataSphere.Interface.BackEnd.TenantMenuManage
{
    /// <summary>
    /// 租户菜单管理数据访问接口
    /// </summary>
    public interface ITenantMenuManageDao : IBaseDao<T_TenantMenu>
    {
        Task<(int count, List<MenuTreeModel> menuListInfo, List<MenuTreeModel> buttonListInfo)> GetTenantMenuPage(GetTenantMenuPageInput input);
    }
}
