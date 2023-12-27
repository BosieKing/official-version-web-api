using Autofac;
using Autofac.Multitenant;
using SharedLibrary.Consts;
using UtilityToolkit.Tools;

namespace WebApi_Offcial.ConfigureServices
{
    /// <summary>
    /// 多租户特殊业务
    /// </summary>
    public static class MultitenantServiceRegister
    {
        /// <summary>
        /// 自定义租户识别策略
        /// </summary>
        /// <remarks>当业务需要需要特殊化的时候，在此方法内注入</remarks>
        public static MultitenantContainer ConfigureMultitenantContainer(IContainer container)
        {
            var tenantIdentifier = new MyTenantIdentificationStrategy(container.Resolve<IHttpContextAccessor>());
            var mtc = new MultitenantContainer(tenantIdentifier, container);

            #region 例子      

            //mtc.ConfigureTenant("PUMC", b => b.RegisterType<PUMCTestServiceImpl>().As<ITestService>().SingleInstance().InstancePerTenant());
            //mtc.ConfigureTenant("SMU", b => b.RegisterType<SMUTestServiceImpl>().As<ITestService>().SingleInstance().InstancePerTenant());

            #endregion


            return mtc;
        }
    }

    /// <summary>
    /// 策略获取租户ID
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
