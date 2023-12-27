using IDataSphere.Extensions;
using IDataSphere.Interface.BackEnd.UserManage;
using IDataSphere.Repositoty;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models.DomainModels;
using UtilityToolkit.Utils;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 用户管理数据访问层
    /// </summary>
    public class UserManageDao : BaseDao<T_User>, IUserManageDao
    {
        #region 构造函数
        public UserManageDao(SqlDbContext dMDbContext) : base(dMDbContext)
        {
        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PaginationResultModel> GetUserPage(GetUserPageInput input)
        {
            var query = from user in dMDbContext.UserRep
                        .Where(!input.NickName.IsNullOrEmpty(), p => EF.Functions.Like(p.NickName, $"%{input.NickName}%"))
                        .Where(!input.Phone.IsNullOrEmpty(), p => EF.Functions.Like(p.Phone, $"%{input.Phone}%"))   
                        select new
                        {
                            user.Id,                       
                            user.NickName,
                            user.Phone,
                            user.Sex,
                            user.Email,
                            user.Code,   
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
        public async Task<List<DropdownSelectionModel>> GetUserRoleList(long userId)
        {
            var ids = await dMDbContext.UserRoleRep.Where(p => p.UserId == userId).Select(p => p.RoleId).ToListAsync();
            var list = await dMDbContext.RoleRep.Select(p => new DropdownSelectionModel
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
            var user = await dMDbContext.UserRep.FirstOrDefaultAsync(p => p.Id == userId);
            user.Password = pwd;
            dMDbContext.UserRep.Update(user);
            await dMDbContext.SaveChangesAsync();
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
            var user = await dMDbContext.UserRep.FirstOrDefaultAsync(p => p.Id == newUser.Id);        
            user.Phone = newUser.Phone;
            user.NickName = newUser.NickName;
            user.Email = newUser.Email;         
            user.Code = newUser.Code;
            user.Sex = newUser.Sex;
            user.IsDisableLogin = newUser.IsDisableLogin;
            dMDbContext.UserRep.Update(user);
            await dMDbContext.SaveChangesAsync();
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
            var user = await dMDbContext.UserRep.FirstOrDefaultAsync(p => p.Id == userId);
            user.IsDisableLogin = isDisableLogin;
            dMDbContext.UserRep.Update(user);
            await dMDbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region 删除

        #endregion
    }
}
