using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.FronDesk;
using Microsoft.EntityFrameworkCore;
using Model.Commons.CoreData;
using Model.Repositotys;

namespace DataSphere.FronDesk
{
    /// <summary>
    /// 前台权限业务数据访问实现类
    /// </summary>
    public class FrontDeskOAuthDao : BaseDao, IFrontDeskOAuthDao
    {
        #region 构造函数
        public FrontDeskOAuthDao(SqlDbContext dbContext) : base(dbContext)
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
            return await dbContext.UserRep.IgnoreTenantFilter().Where(p => p.Phone == phone).OrderByDescending(p => p.LoginTime)
                                      .Join(dbContext.TenantRep, u => u.TenantId, t => t.Id, (u, t) => new TokenInfoModel
                                      {
                                          UserId = u.Id.ToString(),
                                          TenantId = u.TenantId.ToString(),
                                          SchemeName = t.Code.ToString(),
                                          RoleId = string.Join(",", dbContext.UserRoleRep.Where(p => p.UserId == u.Id).Select(p => p.RoleId))
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
            return await dbContext.TenantRep.IgnoreTenantFilter()
                                       .Where(p => p.InviteCode == inviteCode)
                                       .Select(p => new ValueTuple<long, string>(p.Id, p.Name))
                                       .FirstOrDefaultAsync();
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
            var user = await dbContext.UserRep.IgnoreTenantFilter().FirstOrDefaultAsync(p => p.Phone == phone);
            user.Password = newPassword;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateLastLoginTime(long userId)
        {
            T_User user = await dbContext.UserRep.IgnoreTenantFilter().AsTracking().FirstOrDefaultAsync(p => p.Id == userId);
            user.LoginTime = DateTime.Now;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region 删除
        #endregion

    }
}
