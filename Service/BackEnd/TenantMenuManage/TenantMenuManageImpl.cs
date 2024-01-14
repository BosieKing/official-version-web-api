using IDataSphere.Interfaces.BackEnd;
using Mapster;
using Model.Commons.CoreData;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.TenantMenuManage;
using Model.Repositotys;
using SharedLibrary.Enums;

namespace Service.BackEnd.TenantMenuManage
{
    /// <summary>
    /// 后台租户菜单管理业务实现类
    /// </summary>
    public class TenantMenuManageServiceImpl : ITenantMenuManageService
    {
        #region 构造函数
        private readonly ITenantMenuManageDao _tenantMenuManageDao;
        public TenantMenuManageServiceImpl(ITenantMenuManageDao tenantMenuManageDao)
        {
            _tenantMenuManageDao = tenantMenuManageDao;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult> GetPage(GetTenantMenuPageInput input)
        {
            var data = await _tenantMenuManageDao.GetTenantMenuPage(input);
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
        /// 获取目录下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<DropdownDataResult>> GetTenantDirectory()
        {
            return await _tenantMenuManageDao.GetStringList<T_TenantDirectory>();
        }

        #endregion

        #region 新增
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddTenantMenu(AddTenantMenuInput input)
        {
            var data = input.Adapt<T_TenantMenu>();
            data.BrowserPath = input.BrowserPath;
            data.Weight = (int)MenuWeightTypeEnum.Customization;
            return await _tenantMenuManageDao.AddAsync(data);
        }

        /// <summary>
        /// 新增目录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddTenantDirectory(AddTenantDirectoryInput input)
        {
            var data = input.Adapt<T_TenantDirectory>();
            data.BrowserPath = input.BrowserPath;
            return await _tenantMenuManageDao.AddAsync(data);
        }

        /// <summary>
        /// 新增按钮
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddTenantMenuButton(AddTenantMenuButtonInput input)
        {
            var data = input.Adapt<T_TenantMenuButton>();
            return await _tenantMenuManageDao.AddAsync(data);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTenantMenu(UpdateTenantMenuInput input)
        {
            var data = input.Adapt<T_TenantMenu>();
            data.Id = input.Id;
            data.Weight = (int)MenuWeightTypeEnum.Customization;
            return await _tenantMenuManageDao.UpdateAsync(data);
        }

        /// <summary>
        /// 更新目录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTenantDirectory(UpdateTenantDirectoryInput input)
        {
            var data = input.Adapt<T_TenantDirectory>();
            data.Id = input.Id;
            return await _tenantMenuManageDao.UpdateAsync(data);
        }

        /// <summary>
        /// 更新按钮
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTenantMenuButton(UpdateTenantMenuButtonInput input)
        {
            var data = input.Adapt<T_TenantMenuButton>();
            data.Id = input.Id;
            return await _tenantMenuManageDao.UpdateAsync(data);
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTenantMenu(long id)
        {
            return await _tenantMenuManageDao.DeleteAsync<T_TenantMenu>(id);
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTenantDirectory(long id)
        {
            return await _tenantMenuManageDao.DeleteAsync<T_TenantDirectory>(id);
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTenantMenuButton(long id)
        {
            return await _tenantMenuManageDao.DeleteAsync<T_TenantMenuButton>(id);
        }
        #endregion
    }
}
