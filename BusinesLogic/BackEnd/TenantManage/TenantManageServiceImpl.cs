using IDataSphere.Interfaces.BackEnd;
using Mapster;
using Microsoft.AspNetCore.Http;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.TenantManage;
using Model.Repositotys;
using Newtonsoft.Json;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using System.Text;
using UtilityToolkit.Helpers;

namespace BusinesLogic.BackEnd.TenantManage
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
            return await _tenantDao.GetTenantMenuList(input.Id);
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
            Random random = new Random();
            StringBuilder result = new StringBuilder();
            foreach (char item in data)
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
            var result = await CreateInviteCode(tenantId);
            return await _tenantDao.UptateInviteCode(tenantId, result.ToString());
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
            await _tenantDao.AddTenant(tenant);
            // 新增租户对应菜单
            return await _tenantDao.AddTenantMenu(tenant.Id);
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
