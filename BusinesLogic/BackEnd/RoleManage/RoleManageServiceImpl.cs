using BusinesLogic.BackEnd.RoleManage.Dto;
using IDataSphere.Interface.BackEnd.RoleManage;
using IDataSphere.Repositoty;
using Mapster;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using SharedLibrary.Models.DomainModels;
using SharedLibrary.Models.SharedDataModels;
using UtilityToolkit.Helpers;
using UtilityToolkit.Utils;

namespace BusinesLogic.BackEnd.RoleManage
{
    /// <summary>
    /// 角色管理业务层
    /// </summary>
    public class RoleManageServiceImpl : IRoleManageService
    {
        #region 构造函数
        private readonly IRoleManageDao _roleManageDao;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleManageDao"></param>
        public RoleManageServiceImpl(IRoleManageDao roleManageDao)
        {
            _roleManageDao = roleManageDao;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PaginationResultModel> GetRolePage(GetRolePageInput input)
        {
            return await _roleManageDao.GetRolePage(input);
        }

        /// <summary>
        /// 查询角色配置的菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> GetRoleMenuList(IdInput input)
        {
            return await _roleManageDao.GetRoleMenuList(input.Id);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddRole(AddRoleInput input)
        {
            T_Role data = input.Adapt<T_Role>();
            return await _roleManageDao.AddAsync(data);
        }

        /// <summary>
        /// 绑定菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddRoleMenu(AddRoleMenuInput input, string tenantId)
        {
            var routerList = await _roleManageDao.GetList<T_TenantMenu>(p => input.MenuIds.Contains(p.Id), $"{nameof(T_TenantMenu.Router)}");
            List<T_RoleMenu> list = input.MenuIds.Select(p => new T_RoleMenu { RoleId = input.RoleId, MenuId = p }).ToList();
            await _roleManageDao.BatchDeleteAsync<T_RoleMenu>(p => p.RoleId == input.RoleId);
            await _roleManageDao.BatchAddAsync(list);
            string key = BasicDataCacheConst.ROLE_TABLE + tenantId;
            routerList.ForEach(p => { p.Name = p.Name.ToLower(); });
            await RedisMulititionHelper.GetClinet(CacheTypeEnum.BaseData).HMSetAsync(key, input.RoleId.ToString(), routerList.ToJson());
            return true;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRole(UpdateRoleInput input)
        {
            T_Role data = input.Adapt<T_Role>();
            data.Id = input.Id;
            return await _roleManageDao.UpdateAsync(data);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRole(long id)
        {
            // todo：删除角色的时候。同时删除配置的菜单,待测试，完成
            await _roleManageDao.BatchDeleteAsync<T_RoleMenu>(p => p.RoleId == id);
            return await _roleManageDao.DeleteAsync(id);
        }
        #endregion
    }
}
