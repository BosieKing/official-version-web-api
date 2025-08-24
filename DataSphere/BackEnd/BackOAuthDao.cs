using IDataSphere.DatabaseContexts;
using IDataSphere.Interfaces.BackEnd;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Model.Commons.CoreData;
using Model.DTOs.BackEnd.BackEndOAuth;
using Model.Repositotys.Service;
using SharedLibrary.Consts;
using UtilityToolkit.Tools;
using UtilityToolkit.Utils;

namespace DataSphere.BackEnd
{
    /// <summary>
    /// 后台权限管理
    /// </summary>
    public class BackOAuthDao : Repository<T_User>, IBackEndOAuthDao
    {
        #region 构造函数
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BackOAuthDao(SqlDbContext sqlDbContext, IHttpContextAccessor httpContextAccessor) : base(sqlDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion


        #region 登录相关
        /// <summary>
        /// 电话号码的登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> LoginByPassword(BackEndLoginByPasswordInput input)
        {
            string cryptoPasswor = PasswordCryptoUtil.Decrypt(input.Password);
            var user = await _db.ManagerRep.Include(p => p.ManagerRoles)
                .ThenInclude(p => p.Roles)
                .ThenInclude(p => p.RoleMenus)
                .ThenInclude(p => p.Menu)
                .FirstOrDefaultAsync(p => p.Phone == input.Phone && p.Password == cryptoPasswor);
            TokenInfoModel tokenInfo = new();
            tokenInfo.UserId = user.Id.ToString();
            tokenInfo.RoleIds = string.Join(",", user.ManagerRoles
                                                .SelectMany(managerRole => managerRole.Roles)
                                                .SelectMany(role => role.RoleMenus)
                                                .Select(roleMenu => roleMenu.Menu.ControllerRouter)
                                                .ToArray());
            string token = TokenTool.CreateToken(tokenInfo);
            string refreshToken = TokenTool.CreateRefreshToken(tokenInfo, input.IsRemember);
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_Token_Head] = token;
            _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head] = refreshToken;
            return true;
        }


        #endregion

        #region 
        #endregion


        #region 
        #endregion


        #region 
        #endregion
    }
}
