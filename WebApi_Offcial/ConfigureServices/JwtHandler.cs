using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Consts;
using SharedLibrary.Enums;
using SharedLibrary.Models.CoreDataModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using UtilityToolkit.Helpers;
using UtilityToolkit.Tools;
using UtilityToolkit.Utils;

namespace WebApi_Offcial.ConfigureServices
{
    /// <summary>
    /// Jwt 验证处理器  
    /// </summary>
    public class JwtHandler : AuthenticationHandler<JwtBearerOptions>
    {
        private readonly HttpContextAccessor _httpContextAccessor;
        private static JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private static SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigSettingTool.JwtConfigOptions.IssuerSigningKey));

        /// <summary>
        /// 构造函数
        /// </summary>
        public JwtHandler(HttpContextAccessor httpContextAccessor, IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 验证逻辑
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // 获取请求头中的Token信息      
            if (Request.Headers[ClaimsUserConst.HTTP_Token_Head].ToString().IsNullOrWhiteSpace())
            {
                return await Task.FromResult(AuthenticateResult.Fail("Token不能为空"));
            }
            string token = Request.Headers[ClaimsUserConst.HTTP_Token_Head].ToString().Substring("bearer".Length).Trim();
            // 验证Token信息
            if (!jwtSecurityTokenHandler.CanReadToken(token))
            {
                return await Task.FromResult(AuthenticateResult.Fail(""));
            }
            else
            {
                // 配置验证类
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    ValidateIssuer = ConfigSettingTool.JwtConfigOptions.IsValidateIssuer,
                    ValidIssuer = ConfigSettingTool.JwtConfigOptions.Issuer,
                    ValidateAudience = ConfigSettingTool.JwtConfigOptions.IsValidateAudience,
                    ValidAudience = ConfigSettingTool.JwtConfigOptions.Audience,
                    ValidateLifetime = ConfigSettingTool.JwtConfigOptions.IsValidateExpirationTime,
                    ValidateIssuerSigningKey = ConfigSettingTool.JwtConfigOptions.IsValidateIssuerSigningKey,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                };
                try
                {  // 获取令牌完整信息
                    var claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);
                    string userId = claimsPrincipal.FindFirstValue(ClaimsUserConst.USER_ID);
                    // 判断这个token是否已经手动注销->退出、修改密码等情况
                    bool hasBlack = await RedisMulititionHelper.IsWork(userId, token);
                    if (hasBlack)
                    {
                        return await Task.FromResult(AuthenticateResult.Fail(""));
                    }
                    // 有无需要重新获取新token->绑定角色之类的，需要修改token信息
                    if (await RedisMulititionHelper.GetClinet(CacheTypeEnum.User).HExistsAsync(UserCacheConst.MAKE_IN_TABLE, userId))
                    {
                        throw new Exception("");
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        // 判断刷新token有无过期
                        string refreshToken = Request.Headers[ClaimsUserConst.HTTP_REFRESHToken_Head].ToString().Substring("Bearer".Length).Trim();
                        ClaimsPrincipal refreshClaimsPrincipal = jwtSecurityTokenHandler.ValidateToken(refreshToken, parameters, out SecurityToken refreshValidatedToken);
                        // 如果刷新Token合法则重新赋值返回头的普通Token
                        long userId = long.Parse(refreshClaimsPrincipal.Claims.FirstOrDefault(p => p.Type == ClaimsUserConst.USER_ID).Value);
                        long tenantId = long.Parse(refreshClaimsPrincipal.Claims.FirstOrDefault(p => p.Type == ClaimsUserConst.TENANT_ID).Value);
                        string key = UserCacheConst.USER_INFO_TABLE + tenantId;
                        var userInfo = await RedisMulititionHelper.GetClinet(CacheTypeEnum.User).HGetAsync(key, userId.ToString());
                        var user = userInfo.ToObject<TokenInfoModel>();
                        token = TokenTool.CreateToken(user, RedisMulititionHelper.IsSuperManage(user.TenantId));
                        await RedisMulititionHelper.GetClinet(CacheTypeEnum.User).HDelAsync(UserCacheConst.MAKE_IN_TABLE, userId.ToString());
                        _httpContextAccessor.HttpContext.Response.Headers[ClaimsUserConst.HTTP_Token_Head] = token;
                    }
                    catch (Exception)
                    {
                        return await Task.FromResult(AuthenticateResult.Fail(ex));
                    }
                }
            }
            // 解析Token携带的用户信息
            IEnumerable<Claim> claims = jwtSecurityTokenHandler.ReadJwtToken(token).Claims;
            ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
            // 生成票据
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);
            // 颁发证书
            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
