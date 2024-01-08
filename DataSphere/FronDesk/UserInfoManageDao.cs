using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.FronDesk;
using Microsoft.EntityFrameworkCore;
using Model.Commons.Domain;
using Model.Repositotys;

namespace DataSphere.FronDesk
{
    /// <summary>
    /// 前台用户中心数据访问实现类
    /// </summary>
    public class UserInfoManageDao : BaseDao, IUserInfoManageDao
    {
        #region 构造函数
        public UserInfoManageDao(SqlDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region 查询
        /// <summary>
        /// 根据Id查询用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fieldName">需要查询的列名</param>
        /// <returns></returns>
        public async Task<T_User> GetUserInfoById(long userId)
        {
            return await dbContext.UserRep.Where(p => p.Id == userId).Select(p => new T_User
            {
                Id = p.Id,
                Phone = p.Phone,
                NickName = p.NickName,
                Email = p.Email,
                Code = p.Code,
                TenantId = p.TenantId,
                Sex = p.Sex
            }).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 查询用户已绑定的租户平台下拉
        /// </summary>
        /// <param name="uniqueNumber"></param>
        /// <returns></returns>
        public async Task<List<DropdownDataModel>> GetTenantBindList(long userId)
        {
            var userPhones = dbContext.UserRep.IgnoreTenantFilter().Where(p => p.Id == userId)
                                           .Select(p => p.Phone);
            var tenantIdsQuery = dbContext.UserRep.IgnoreTenantFilter().Where(p => userPhones.Contains(p.Phone)).Select(p => p.TenantId);
            return await dbContext.TenantRep.Where(p => tenantIdsQuery.Contains(p.Id)).Select(p => new DropdownDataModel { Id = p.Id, Name = p.Name }).ToListAsync();
        }

        /// <summary>
        /// 根据id查询用户电话号码和密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<(string Password, string Phone)> GetUserPassword(long userId)
        {
            return await dbContext.UserRep.Where(p => p.Id == userId).Select(p => new ValueTuple<string, string>(p.Password, p.Phone))
                                   .FirstOrDefaultAsync();
        }
        #endregion            

        #region 更新
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserInfo(T_User input)
        {
            var user = await dbContext.UserRep.FirstOrDefaultAsync(p => p.Id == input.Id);
            user.NickName = input.NickName;
            user.Email = input.Email;
            user.Code = input.Code;
            user.Sex = input.Sex;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAvatar(string url, long userId)
        {
            var data = await dbContext.UserRep.FirstOrDefaultAsync(p => p.Id == userId);
            data.AvatarUrl = url;
            dbContext.UserRep.Update(data);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePassword(string newPassword, long userId)
        {
            var data = await dbContext.UserRep.FirstOrDefaultAsync(p => p.Id == userId);
            data.Password = newPassword;
            dbContext.UserRep.Update(data);
            await dbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region 删除
        #endregion

    }
}
