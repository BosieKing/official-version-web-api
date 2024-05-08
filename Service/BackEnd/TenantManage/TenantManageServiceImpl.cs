using IDataSphere.Interfaces.BackEnd;
using Mapster;
using Microsoft.AspNetCore.Http;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.TenantManage;
using Model.Repositotys.BasicData;
using Model.Repositotys.Service;
using Newtonsoft.Json;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using System.Text;
using UtilityToolkit.Helpers;
using UtilityToolkit.Utils;

namespace Service.BackEnd.TenantManage
{
    /// <summary>
    /// 后台租户管理业务实现类
    /// </summary>
    public class TenantManageServiceImpl : ITenantManageService
    {
        #region 构造函数
        private readonly ITenantManageDao _tenantDao;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TenantManageServiceImpl(ITenantManageDao tenantDao, IHttpContextAccessor httpContextAccessor)
        {
            _tenantDao = tenantDao;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult> GetTenantPage(GetTenantPageInput input)
        {
            return await _tenantDao.GetTenantPage(input);
        }

        /// <summary>
        /// 查询租户已配置的菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<DropdownSelectionResult>> GetTenantMenuList(IdInput input)
        {
            List<long> uniqueNumbers = await _tenantDao.GetLongFields<T_TenantMenu>(t => t.TenantId == input.Id, nameof(T_TenantMenu.UniqueNumber));

            return await _tenantDao.GetCheckList<T_Menu>(nameof(T_Menu.UniqueNumber), uniqueNumbers,
                                                             p => p.Weight == (int)MenuWeightTypeEnum.Service || p.Weight == (int)MenuWeightTypeEnum.Customization);
        }

        /// <summary>
        /// 查询租户的目录下拉
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<List<DropdownDataResult>> GetTenantDirectoryList(long tenantId)
        {
            return await _tenantDao.GetStringList<T_TenantDirectory>(p => p.TenantId == tenantId, isIgnoreTenant: true);
        }
        #endregion

        #region 新增

        /// <summary>
        /// 生成邀请码
        /// </summary>
        /// <returns></returns>
        private async Task<string> CreateInviteCode(long tenantId)
        {
            long i = 1;
            // 转成数组输出，每次输出不同的数组
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= b + 1;
            }
            string data = string.Format("{0:x}", i - DateTime.Now.Ticks + tenantId);
            Random random = new();
            StringBuilder result = new();
            foreach (var item in data)
            {
                int number = random.Next(0, 2);
                if (number == 0)
                {
                    result.Append(item.ToString().ToLower());
                    continue;
                }
                result.Append(item);
            }
            var redisClient = RedisMulititionHelper.GetClinet(CacheTypeEnum.BaseData);
            redisClient.HMSet(BasicDataCacheConst.TENANT_TABLE, tenantId.ToString(), JsonConvert.SerializeObject(new
            {
                Id = tenantId,
                InviteCode = result.ToString()
            }));
            return result.ToString();
        }

        /// <summary>
        /// 重新生成邀请码
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<bool> UptateInviteCode(long tenantId)
        {
            string code = await CreateInviteCode(tenantId);
            return await _tenantDao.UptateInviteCode(tenantId, code.ToString());
        }

        /// <summary>
        /// 新增租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddTenant(AddTenantInput input)
        {
            T_Tenant tenant = input.Adapt<T_Tenant>();
            tenant.InviteCode = await CreateInviteCode(tenant.Id);
            // 新增租户
            await _tenantDao.AddAsync(tenant);
            // 新增租户的时候增加对应后台管理员用户
            T_User user = input.Adapt<T_User>();
            user.NickName = user.NickName;
            user.Password = input.Password;
            user.TenantId = tenant.Id;
            user.CreatedUserId = user.Id;
            await _tenantDao.AddAsync<T_User>(user);
            // 新增租户对应菜单
            List<(long Id, string Router)> menuIds = await _tenantDao.AddTenantMenu(tenant.Id, user.Id);
            // 新增租户的后台管理员角色
            T_Role role = new T_Role();
            role.Name = tenant.Name + "后台管理员";
            role.CreatedUserId = user.Id;
            role.TenantId = tenant.Id;
            await _tenantDao.AddAsync<T_Role>(role);
            // 配置对应的菜单
            List<T_RoleMenu> menus = menuIds.Select(p => new T_RoleMenu { RoleId = role.Id, MenuId = p.Id, TenantId = tenant.Id, CreatedUserId = user.Id }).ToList();
            await _tenantDao.BatchAddAsync<T_RoleMenu>(menus);
            // 缓存
            string key = BasicDataCacheConst.ROLE_TABLE + tenant.Id;
            menuIds.ForEach(p => { p.Router = p.Router.ToLower(); });
            await RedisMulititionHelper.GetClinet(CacheTypeEnum.BaseData).HMSetAsync(key, role.Id.ToString(), menuIds.ToJson());
            // 配置角色与用户的关系
            T_UserRole userRole = new T_UserRole();
            userRole.UserId = user.Id;
            userRole.RoleId = role.Id;
            userRole.TenantId = tenant.Id;
            userRole.CreatedUserId = user.Id;
            return await _tenantDao.AddAsync<T_UserRole>(userRole);
        }

        /// <summary>
        /// 推送新菜单给租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> PushTenantMenu(PushTenantMenuInput input)
        {
            return await _tenantDao.PushTenantMenu(input.MenuId, input.DirectoryId, input.TenantId);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 修改租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTenant(UpdateTenantInput input)
        {
            return await _tenantDao.UpdateTenant(input.Name, input.Code, input.Id);
        }


        #endregion

        #region 删除
        #endregion
    }
}
