using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.FronDesk;
using Microsoft.EntityFrameworkCore;
using Model.Repositotys.Service;

namespace DataSphere.FronDesk
{
    /// <summary>
    /// 前台用户中心数据访问实现类
    /// </summary>
    public class UserInfoManageDao : BaseDao<T_User>, IUserInfoManageDao
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
        public async Task<dynamic> GetUserInfoById(long userId)
        {
            return await dbContext.UserRep.Where(p => p.Id == userId).Select(p => new T_User
            {
                Id = p.Id,
                Phone = p.Phone,
                NickName = p.NickName,
                Email = p.Email,
                Sex = p.Sex
            }).FirstOrDefaultAsync();
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
            T_User user = await dbContext.UserRep.AsTracking().FirstOrDefaultAsync(p => p.Id == input.Id);
            user.NickName = input.NickName;
            user.Email = input.Email;
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
            T_User user = await dbContext.UserRep.AsTracking().FirstOrDefaultAsync(p => p.Id == userId);
            user.AvatarUrl = url;
            dbContext.UserRep.Update(user);
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
            T_User user = await dbContext.UserRep.AsTracking().FirstOrDefaultAsync(p => p.Id == userId);
            user.Password = newPassword;
            dbContext.UserRep.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region 删除
        #endregion

    }
}
