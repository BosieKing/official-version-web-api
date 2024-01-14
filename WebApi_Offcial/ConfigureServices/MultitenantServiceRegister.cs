using Autofac;
using Autofac.Multitenant;

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
        /// <param name="container"></param>
        /// <returns></returns>
        /// <remarks>当Service需要需要特殊化的时候，在此方法内注入</remarks>
        public static MultitenantContainer ConfigureMultitenantContainer(IContainer container)
        {
            MyTenantIdentificationStrategy tenantIdentifier = new(container.Resolve<IHttpContextAccessor>());
            MultitenantContainer mtc = new(tenantIdentifier, container);

            #region 例子      

            //mtc.ConfigureTenant("PUMC", b => b.RegisterType<PUMCTestServiceImpl>().As<ITestService>().SingleInstance().InstancePerTenant());
            //mtc.ConfigureTenant("SMU", b => b.RegisterType<SMUTestServiceImpl>().As<ITestService>().SingleInstance().InstancePerTenant());

            #endregion

            return mtc;
        }
    }

}
