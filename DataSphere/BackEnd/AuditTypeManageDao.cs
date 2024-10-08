using Model.Repositotys.BasicData;
using Microsoft.EntityFrameworkCore;
using UtilityToolkit.Utils;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.BackEnd.AuditTypeManage;
using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.BackEnd;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 审核角色类型配置表数据访问层
    /// </summary>
    public class AuditTypeManageDao : BaseDao<T_AuditType>, IAuditTypeManageDao
    {      
        #region 构造函数
        public AuditTypeManageDao(SqlDbContext dbContext) : base(dbContext)
        {

        }
        #endregion

        #region 查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult> GetAuditTypePage(GetAuditTypePageInput input) 
        {
             var query = from data in dbContext.AuditTypeRep
                        .Where(!input.Name.IsNullOrEmpty(), p => EF.Functions.Like(p.Name, $"%{input.Name}%"))
                        join createUser in dbContext.UserRep on data.CreatedUserId equals createUser.Id into createUserResult
                        from createUser in createUserResult.DefaultIfEmpty()
                        join updateUser in dbContext.UserRep on data.UpdateUserId equals updateUser.Id into updateUserResult
                        from updateUser in updateUserResult.DefaultIfEmpty()
                        select new
                        {
                            Id = data.Id,
                            Name = data.Name,
                            CreatedName = createUser.NickName,
                            CreatedTime = data.CreatedTime,
                            UpdateUserName = updateUser.NickName,
                            UpdateTime = data.UpdateTime
                        };
            return await base.AdaptPage(query, input.PageSize, input.PageNo);
        }
        #endregion

        #region 新增
      
        #endregion

        #region 更新
     
        #endregion

        #region 删除
        
        #endregion
    }
}
