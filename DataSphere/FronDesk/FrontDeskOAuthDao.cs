using IDataSphere.Extensions;
using IDataSphere.Interface.FronDesk;
using IDataSphere.Repositoty;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models.CoreDataModels;

namespace DataSphere.FronDesk
{
    /// <summary>
    /// 前台权限数据库访问类
    /// </summary>
    public class FrontDeskOAuthDao : BaseDao<T_User>, IFrontDeskOAuthDao
    {
        #region 构造函数
        public FrontDeskOAuthDao(SqlDbContext dMDbContext) : base(dMDbContext)
        {
        }
        #endregion

        #region 查询
        /// <summary>
        /// 根据电话号码获取最近登录租户平台的用户信息
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<TokenInfoModel> GetUserInfoByPhone(string phone)
        {
            // 获取用户最近一次登陆的租户账号
            return await dMDbContext.UserRep.IgnoreTenantFilter().Where(p => p.Phone == phone).OrderByDescending(p => p.LoginTime)
                                      .Join(dMDbContext.TenantRep, u => u.TenantId, t => t.Id, (u, t) => new TokenInfoModel
                                      {
                                          UserId = u.Id.ToString(),
                                          TenantId = u.TenantId.ToString(),
                                          SchemeName = t.Code.ToString(),
                                          UniqueNumber = u.UniqueNumber.ToString(),
                                          RoleId = string.Join(",", dMDbContext.UserRoleRep.Where(p => p.UserId == u.Id).Select(p => p.RoleId))
                                      }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 根据邀请码获取租户id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inviteCode"></param>
        /// <returns></returns>
        public async Task<(long Id, string Code)> GetIdByInviteCode(string inviteCode)
        {
            var id = await dMDbContext.TenantRep.IgnoreTenantFilter()
                                       .Where(p => p.InviteCode == inviteCode)
                                       .Select(p => new ValueTuple<long, string>(p.Id, p.Name))
                                       .FirstOrDefaultAsync();
            return id;
        }

        /// <summary>
        /// 查询电话号码是否存在
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns>true已注册，false未注册</returns>
        public async Task<bool> PhoneExiste(string phone, long tenantId = 0)
        {
            return await dMDbContext.UserRep.IgnoreTenantFilter().Where(p => p.Phone == phone)
                                   .Where(!tenantId.Equals(0), p => p.TenantId == tenantId)
                                   .AnyAsync();
        }

        /// <summary>
        /// 判断电话号码和密码是否匹配（无租户限制）
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> PassWordExiste(string phone, string password)
        {
            return await dMDbContext.UserRep.IgnoreTenantFilter().Where(p => p.Phone == phone && p.Password == password)
                                   .AnyAsync();
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="user"></param>
        /// <param name="inviteCode"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(T_User input)
        {
            return await AddAsync(input);
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<bool> UpdateLastLoginPassWord(string phone, string newPassword)
        {
            var user = await dMDbContext.UserRep.IgnoreTenantFilter().FirstOrDefaultAsync(p => p.Phone == phone);
            user.Password = newPassword;
            dMDbContext.Update(user);
            await dMDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateLastLoginTime(long userId)
        {
            var user = await dMDbContext.UserRep.IgnoreTenantFilter().FirstOrDefaultAsync(p => p.Id == userId);
            user.LoginTime = DateTime.Now;
            dMDbContext.Update(user);
            await dMDbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region 删除
        #endregion

    }
}
