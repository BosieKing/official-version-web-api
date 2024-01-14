using Autofac.Multitenant;
using SharedLibrary.Consts;
using UtilityToolkit.Tools;

namespace WebApi_Offcial.ConfigureServices
{
    /// <summary>
    /// 获取租户ID策略
    /// </summary>
    public class MyTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        private IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public MyTenantIdentificationStrategy(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 尝试从当前上下文获取标识的租户。
        /// </summary>
        /// <param name="SchemeName">来源于Token中的SchemeName</param>
        /// <returns></returns>
        public bool TryIdentifyTenant(out object SchemeName)
        {
            SchemeName = null;
            try
            {
                if (httpContextAccessor.HttpContext != null)
                {
                    string token = httpContextAccessor.HttpContext.Request.Headers[ClaimsUserConst.HTTP_Token_Head].ToString();
                    var claims = TokenTool.GetClaims(token).ToList();
                    SchemeName = "";
                }
            }
            catch (Exception)
            {

            }
            return SchemeName != null;
        }
    }
}
