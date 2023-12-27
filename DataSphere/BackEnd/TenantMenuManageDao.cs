using IDataSphere.Interface.BackEnd.TenantMenuManage;
using IDataSphere.Repositoty;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Enums;
using SharedLibrary.Models.CoreDataModels;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 租户菜单管理数据访问层
    /// </summary>
    public class TenantMenuManageDao : BaseDao<T_TenantMenu>, ITenantMenuManageDao
    {
        #region 构造函数
        public TenantMenuManageDao(SqlDbContext dMDbContext) : base(dMDbContext)
        {
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<(int count, List<MenuTreeModel> menuListInfo, List<MenuTreeModel> buttonListInfo)> GetTenantMenuPage(GetTenantMenuPageInput input)
        {
            int count = await dMDbContext.TenantDirectoryRep.CountAsync();
            var menuInfo = dMDbContext.TenantDirectoryRep.Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize)
                                          .GroupJoin(dMDbContext.TenantMenuRep, d => d.Id, m => m.DirectoryId, (d, m) => new { d, m })
                                          .SelectMany(dm => dm.m.DefaultIfEmpty(), (dm, m) => new MenuTreeModel
                                          {
                                              Id = m == null ? 0 : m.Id,
                                              Name = m == null ? "" : m.Name,
                                              Icon = m == null ? "" : m.Icon,
                                              Router = m == null ? "" : m.Router,
                                              Path = m == null ? "" : m.StrPath,
                                              IsHidden = m == null ? true : m.IsHidden,
                                              Weight = m == null ? 0 : m.Weight,
                                              Remark = m == null ? "" : m.Remark,
                                              Component = m == null ? "" : m.Component,
                                              PId = dm.d.Id,
                                              PName = dm.d.Name,
                                              PIcon = dm.d.Icon,
                                              PPath = dm.d.StrPath,
                                          })
                                          .ToList();
            var buttonList = dMDbContext.TenantMenuButtonRep.Where(p => menuInfo.Select(m => m.Id).Contains(p.MenuId)).Select(p => new MenuTreeModel
            {
                Id = p.Id,
                Name = p.Name,
                Router = p.ActionName,
                PId = p.MenuId,
                Type = (int)MenuTreeTypeEnum.Button
            }).ToList();
            return new ValueTuple<int, List<MenuTreeModel>, List<MenuTreeModel>>(count, menuInfo, buttonList);
        }
        #endregion

        #region 新增

        #endregion

        #region 更新

        #endregion

        #region 删除

        #endregion

    }
}
