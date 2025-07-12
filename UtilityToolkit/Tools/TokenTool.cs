using Microsoft.IdentityModel.Tokens;
using Model.Commons.CoreData;
using SharedLibrary.Consts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UtilityToolkit.Tools
{
    /// <summary>
    /// Token工具类
    /// </summary>
    public static class TokenTool
    {
        /// <summary>
        /// 根据用户信息生成Token
        /// </summary>
        /// <returns>普通Token</returns>
        public static string CreateToken(TokenInfoModel tokenResult, bool IsSuperManage = false)
        {
            // 储存身份信息
            List<Claim> claimList = new List<Claim>();
            // payload必须属性
            // 签发方
            claimList.Add(new Claim(JwtRegisteredClaimNames.Iss, ConfigSettingTool.JwtConfigOptions.Issuer));
            // 接收方
            claimList.Add(new Claim(JwtRegisteredClaimNames.Aud, ConfigSettingTool.JwtConfigOptions.Audience));
            // 过期时间
            claimList.Add(new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(ConfigSettingTool.JwtConfigOptions.ExpirationTime)).ToUnixTimeSeconds()}"));
            // 当前时间
            claimList.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.Ticks.ToString()));
            // 标识
            claimList.Add(new Claim(JwtRegisteredClaimNames.Jti, tokenResult.UserId));

            // 自定义携带内容
            claimList.Add(new Claim(ClaimsUserConst.USER_ID, tokenResult.UserId));
            claimList.Add(new Claim(ClaimsUserConst.ROLE_IDs, tokenResult.RoleIds));
            claimList.Add(new Claim(ClaimsUserConst.SCHEME_NAME, tokenResult.SchemeName));
            if (IsSuperManage)
            {
                claimList.Add(new Claim(ClaimsUserConst.IS_SUPERMANAGE, "true"));
            }
            // 获取密钥
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigSettingTool.JwtConfigOptions.IssuerSigningKey));
            // 定义签名方式
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 生成token
            JwtSecurityToken token = new JwtSecurityToken(claims: claimList, signingCredentials: signingCredentials);
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            return jwtHandler.WriteToken(token);
        }

        /// <summary>
        /// 根据用户信息生成刷新Token
        /// </summary>
        /// <returns>刷新Token</returns>
        public static string CreateRefreshToken(TokenInfoModel tokenResult, bool isRemember)
        {
            // 储存身份信息
            List<Claim> claimList = new List<Claim>();
            // payload必须属性
            // 签发方
            claimList.Add(new Claim(JwtRegisteredClaimNames.Iss, ConfigSettingTool.JwtConfigOptions.Issuer));
            // 接收方
            claimList.Add(new Claim(JwtRegisteredClaimNames.Aud, ConfigSettingTool.JwtConfigOptions.Audience));
            // 过期时间
            int exTime = isRemember ? ConfigSettingTool.JwtConfigOptions.RefreshTokenExpirationTime * 7 : ConfigSettingTool.JwtConfigOptions.RefreshTokenExpirationTime;
            claimList.Add(new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(exTime)).ToUnixTimeSeconds()}"));
            // 当前时间
            claimList.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.Ticks.ToString()));
            // 标识
            claimList.Add(new Claim(JwtRegisteredClaimNames.Jti, tokenResult.UserId));

            // 自定义携带内容
            claimList.Add(new Claim(ClaimsUserConst.USER_ID, tokenResult.UserId));
            // 获取密钥
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigSettingTool.JwtConfigOptions.IssuerSigningKey));
            // 定义签名方式
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 生成token
            JwtSecurityToken token = new JwtSecurityToken(claims: claimList, signingCredentials: signingCredentials);
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            return jwtHandler.WriteToken(token);
        }

        /// <summary>
        /// 强制注销Token
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool CancelToken(string key)
        {
            return true;
        }

        /// <summary>
        /// 根据token解析Claims（不验证过期时间）
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> GetClaims(string token)
        {
            // 获取令牌完整信息
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            IEnumerable<Claim> claims = jwtHandler.ReadJwtToken(token.Substring("bearer".Length).Trim()).Claims;
            return claims;
        }
    }
}
