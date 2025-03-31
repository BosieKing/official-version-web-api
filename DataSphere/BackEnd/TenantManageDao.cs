using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.BackEnd;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Commons.Domain;
using Model.DTOs.BackEnd.TenantManage;
using Model.Repositotys.BasicData;
using Model.Repositotys.Service;
using SharedLibrary.Enums;
using UtilityToolkit.Utils;
using Yitter.IdGenerator;
namespace DataSphere.BackEnd
{
    /// <summary>
    /// 后台租户管理数据访问实现类
    /// </summary>
    public class TenantManageDao : BaseDao<T_Tenant>, ITenantManageDao
    {
        #region 构造函数
        public TenantManageDao(SqlDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region 查询数据
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult> GetTenantPage(GetTenantPageInput input)
        {
            var query = from tenant in dbContext.TenantRep
                        .WhereIfEmpty(input.Name, p => EF.Functions.Like(p.Name, $"%{input.Name}%"))
                        .WhereIfEmpty(input.Code, p => EF.Functions.Like(p.Code, $"%{input.Code}%"))
                        join createUser in dbContext.UserRep on tenant.CreatedUserId equals createUser.Id into createUserResult
                        from createUser in createUserResult.DefaultIfEmpty()
                        join updateUser in dbContext.UserRep on tenant.UpdateUserId equals updateUser.Id into updateUserResult
                        from updateUser in updateUserResult.DefaultIfEmpty()
                        select new
                        {
                            tenant.Id,
                            tenant.Name,
                            tenant.Code,
                            tenant.InviteCode,
                            CreatedName = createUser.NickName,
                            tenant.CreatedTime,
                            UpdateUserName = updateUser.NickName,
                            tenant.UpdateTime
                        };
            return await base.AdaptPage(query, input.PageSize, input.PageNo);

        }
        #endregion

        #region 新增      
        #endregion

        #region 修改
        /// <summary>
        /// 更新邀请码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inviteCode"></param>
        /// <returns></returns>
        public async Task<bool> UptateInviteCode(long id, string inviteCode)
        {
            T_Tenant tenant = await dbContext.TenantRep.FirstOrDefaultAsync(p => p.Id == id);
            tenant.InviteCode = inviteCode;
            dbContext.TenantRep.Update(tenant);
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 修改租户信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateTenant(string name, string code, long Id)
        {
            T_Tenant tenant = await dbContext.TenantRep.FirstOrDefaultAsync(p => p.Id == Id);
            tenant.Name = name;
            tenant.Code = code;
            dbContext.TenantRep.Update(tenant);
            await dbContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
