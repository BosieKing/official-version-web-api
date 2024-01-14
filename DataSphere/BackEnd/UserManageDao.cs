using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.BackEnd;
using Microsoft.EntityFrameworkCore;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.UserManage;
using Model.Repositotys;
using UtilityToolkit.Utils;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 后台用户管理数据访问实现类
    /// </summary>
    public class UserManageDao : BaseDao, IUserManageDao
    {
        #region 构造函数
        public UserManageDao(SqlDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult> GetUserPage(GetUserPageInput input)
        {
            var query = from user in dbContext.UserRep
                        .Where(!input.NickName.IsNullOrEmpty(), p => EF.Functions.Like(p.NickName, $"%{input.NickName}%"))
                        .Where(!input.Phone.IsNullOrEmpty(), p => EF.Functions.Like(p.Phone, $"%{input.Phone}%"))
                        select new
                        {
                            user.Id,
                            user.NickName,
                            user.Phone,
                            user.Sex,
                            user.Email,
                            user.IsDisableLogin,
                            user.CreatedTime,
                            user.UpdateTime
                        };
            return await base.AdaptPage(query, input.PageSize, input.PageNo);
        }

        /// <summary>
        /// 查询角色集合
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mentorId"></param>
        /// <returns></returns>
        public async Task<List<DropdownSelectionResult>> GetUserRoleList(long userId)
        {
            var ids = await dbContext.UserRoleRep.Where(p => p.UserId == userId).Select(p => p.RoleId).ToListAsync();
            var list = await dbContext.RoleRep.Select(p => new DropdownSelectionResult
            {

                Id = p.Id,
                Name = p.Name,
                IsCheck = ids.Contains(p.Id)
            }).ToListAsync();
            return list;
        }
        #endregion
         
        #region 新增

        #endregion

        #region 更新  
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<bool> ResetPassword(long userId, string pwd)
        {
            var user = await dbContext.UserRep.FirstOrDefaultAsync(p => p.Id == userId);
            user.Password = pwd;
            dbContext.UserRep.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }


        /// <summary>
        /// 更新用户数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(T_User newUser)
        {
            var user = await dbContext.UserRep.FirstOrDefaultAsync(p => p.Id == newUser.Id);
            user.Phone = newUser.Phone;
            user.NickName = newUser.NickName;
            user.Email = newUser.Email;
            user.Sex = newUser.Sex;
            user.IsDisableLogin = newUser.IsDisableLogin;
            dbContext.UserRep.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// 修改是否允许登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<bool> UpdateIsDisableLogin(long userId, bool isDisableLogin)
        {
            var user = await dbContext.UserRep.FirstOrDefaultAsync(p => p.Id == userId);
            user.IsDisableLogin = isDisableLogin;
            dbContext.UserRep.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region 删除

        #endregion
    }
}
