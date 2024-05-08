using Model.Commons.CoreData;
using Model.DTOs.BackEnd.MenuManage;
using Model.Repositotys.BasicData;

namespace IDataSphere.Interfaces.BackEnd
{
    /// <summary>
    /// 后台菜单库管理数据访问接口
    /// </summary>
    /// <remarks>T_Menu表</remarks>
    public interface IMenuManageDao : IBaseDao<T_Menu>
    {
        Task<(int count, List<MenuTreeModel> menuListInfo, List<MenuTreeModel> buttonListInfo)> GetMenuPage(GetMenuPageInput input);
    }
}
