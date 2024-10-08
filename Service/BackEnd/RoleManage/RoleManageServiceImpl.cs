﻿using IDataSphere.Interfaces.BackEnd;
using Mapster;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.RoleManage;
using Model.Repositotys.BasicData;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using UtilityToolkit.Helpers;
using UtilityToolkit.Utils;
namespace Service.BackEnd.RoleManage
{
    /// <summary>
    /// 后台角色管理业务实现类
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
        public async Task<PageResult> GetRolePage(GetRolePageInput input)
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
            T_Role role = input.Adapt<T_Role>();
            return await _roleManageDao.AddAsync(role);
        }

        /// <summary>
        /// 绑定菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddRoleMenu(AddRoleMenuInput input, string tenantId)
        {
            List<DropdownDataResult> routers = await _roleManageDao.GetStringList<T_TenantMenu>(p => input.MenuIds.Contains(p.Id), $"{nameof(T_TenantMenu.ControllerRouter)}");
            List<T_RoleMenu> list = input.MenuIds.Select(p => new T_RoleMenu { RoleId = input.RoleId, MenuId = p }).ToList();
            await _roleManageDao.BatchDeleteAsync<T_RoleMenu>(p => p.RoleId == input.RoleId);
            await _roleManageDao.BatchAddAsync(list);
            string key = BasicDataCacheConst.ROLE_TABLE + tenantId;
            routers.ForEach(p =>
            {
                p.Name = p.Name.ToLower();
            });
            await RedisMulititionHelper.GetClient(CacheTypeEnum.BaseData).HMSetAsync(key, input.RoleId.ToString(), routers.ToJson());
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
            T_Role role = input.Adapt<T_Role>();
            role.Id = input.Id;
            return await _roleManageDao.UpdateAsync(role);
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
            await _roleManageDao.BatchDeleteAsync<T_RoleMenu>(p => p.RoleId == id);
            return await _roleManageDao.DeleteAsync<T_Role>(id);
        }
        #endregion
    }
}
