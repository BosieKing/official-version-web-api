using IDataSphere.Repositoty;
using SharedLibrary.Models.CoreDataModels;

namespace IDataSphere.Interface.BackEnd.MenuManage
{
    public interface IMenuManageDao : IBaseDao<T_Menu>
    {
        Task<(int count, List<MenuTreeModel> menuListInfo, List<MenuTreeModel> buttonListInfo)> GetMenuPage(GetMenuPageInput input);
    }
}
