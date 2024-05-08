using IDataSphere.Interfaces.BackEnd;
using Mapster;
using Model.Commons.CoreData;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.MenuManage;
using Model.Repositotys.BasicData;
using SharedLibrary.Enums;

namespace Service.BackEnd.MenuManage
{
    /// <summary>
    /// 后台菜单库管理业务实现类
    /// </summary>
    public class MenuManageServiceImpl : IMenuManageService
    {
        #region 构造函数
        private readonly IMenuManageDao _menuManageDao;
        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuManageServiceImpl(IMenuManageDao menuManageDao)
        {
            _menuManageDao = menuManageDao;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult> GetMenuPage(GetMenuPageInput input)
        {
            var data = await _menuManageDao.GetMenuPage(input);
            var result = data.menuListInfo.GroupBy(p => new { p.PId, p.PName, p.PIcon, p.PPath })
                             .Select(d => new
                             {
                                 PId = 0,
                                 Id = d.Key.PId,
                                 Name = d.Key.PName,
                                 Icon = d.Key.PIcon,
                                 Router = "/",
                                 Path = d.Key.PPath,
                                 Component = "/",
                                 Weight = 0,
                                 Type = (int)MenuTreeTypeEnum.Directory,
                                 Children = d.Where(m => !m.Id.Equals(0)).GroupBy(m => new { m.Id, m.Remark, m.Name, m.Router, m.Component, m.BrowserPath, m.IsHidden, m.Icon, m.Weight }).Select(m => new
                                 {
                                     d.Key.PId,
                                     m.Key.Id,
                                     m.Key.Name,
                                     m.Key.Icon,
                                     m.Key.Router,
                                     m.Key.Weight,
                                     m.Key.BrowserPath,
                                     m.Key.IsHidden,
                                     m.Key.Remark,
                                     m.Key.Component,
                                     Type = (int)MenuTreeTypeEnum.Menu,
                                     Children = data.buttonListInfo.Where(b => m.Key.Id == b.PId).Select(r => r.Adapt<MenuTreeModel>())
                                 })
                             }).ToList();
            return new PageResult(input.PageNo, input.PageSize, data.count, result);
        }

        /// <summary>
        /// 查询目录下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<DropdownDataResult>> GetDirectoryList()
        {
            return await _menuManageDao.GetStringList<T_Directory>();
        }

        /// <summary>
        /// 查询菜单下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<DropdownDataResult>> GetMenuList()
        {
            return await _menuManageDao.GetStringList<T_Menu>();
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增目录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddDirectory(AddDirectoryInput input)
        {
            T_Directory dir = input.Adapt<T_Directory>();
            dir.BrowserPath = input.BrowserPath;
            return await _menuManageDao.AddAsync(dir);
        }


        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddMenu(AddMenuInput input)
        {
            T_Menu menu = input.Adapt<T_Menu>();
            menu.UniqueNumber = menu.Id;
            menu.BrowserPath = input.BrowserPath;
            return await _menuManageDao.AddAsync(menu);
        }

        /// <summary>
        /// 绑定按钮
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddMenuButton(AddMenuButtonInput input)
        {
            T_MenuButton button = input.Adapt<T_MenuButton>();
            return await _menuManageDao.AddAsync(button);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新目录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateDirectory(UpdateDirectoryInput input)
        {
            T_Directory dir = input.Adapt<T_Directory>();
            dir.Id = input.Id;
            return await _menuManageDao.UpdateAsync(dir);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMenu(UpdateMeunInput input)
        {
            T_Menu menu = input.Adapt<T_Menu>();
            menu.Id = input.Id;
            menu.UniqueNumber = menu.Id;
            return await _menuManageDao.UpdateAsync(menu);
        }

        /// <summary>
        /// 更新按钮
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMenuButton(UpdateMenuButtonInput input)
        {
            T_MenuButton button = input.Adapt<T_MenuButton>();
            button.Id = input.Id;
            return await _menuManageDao.UpdateAsync(button);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteDirectory(long id)
        {
            return await _menuManageDao.DeleteAsync<T_Directory>(id);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> DeleteMenu(long id)
        {
            return await _menuManageDao.DeleteAsync<T_Menu>(id);
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteMenuButton(long id)
        {
            return await _menuManageDao.DeleteAsync<T_MenuButton>(id);
        }
        #endregion
    }
}
