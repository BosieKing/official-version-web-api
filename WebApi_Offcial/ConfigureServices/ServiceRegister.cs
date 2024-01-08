using Autofac;
using IDataSphere.DatabaseContexts;

using SharedLibrary.Consts;
using System.Reflection;
using System.Runtime.Loader;

namespace WebApi_Offcial.ConfigureServices
{
    /// <summary>
    /// 依赖注入模块
    /// </summary>
    public class ServiceRegister : Autofac.Module
    {
        private const string ServiceAssemblyName = "BusinesLogic";

        private const string DaoAssemblyName = "DataSphere";

        private const string WebAPIAssemblyName = "WebApi_Offcial";
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //// 程序集服务类注入，作用域
            //builder.RegisterAssemblyTypes(GetAssembly(ServiceAssemblyName))
            //       .Where(p => p.Name.EndsWith("Service"))
            //       .InstancePerLifetimeScope();

            // 程序集服务类注入，作用域
            builder.RegisterAssemblyTypes(GetAssembly(WebAPIAssemblyName))
                   .Where(p => p.Name.EndsWith("ActionFilter"))
                   .InstancePerLifetimeScope();

            // 程序集实现类与接口注入，作用域
            builder.RegisterAssemblyTypes(GetAssembly(ServiceAssemblyName))
                   .Where(p => p.Name.EndsWith("ServiceImpl"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            // 程序集实现类与接口注入，作用域
            builder.RegisterAssemblyTypes(GetAssembly(DaoAssemblyName))
                   .Where(p => p.Name.EndsWith("Dao"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            // 单个类注入，作用域
            builder.RegisterType<HttpContextAccessor>().InstancePerLifetimeScope();
            // 注册用户信息类
            builder.Register<UserProvider>(p =>
            {
                // 每次请求序列化一个新的附加有当前用户信息的类
                var _httpContextAccessor = p.Resolve<HttpContextAccessor>();
                string tenantId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.TENANT_ID)?.Value;
                string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsUserConst.USER_ID)?.Value;
                return new UserProvider(long.Parse(tenantId ?? "0"), long.Parse(userId ?? "0")); ;
            }).InstancePerLifetimeScope();
            // 注册数据库上下文实例化工厂
            builder.RegisterType<SqlDbContextFactory>().InstancePerLifetimeScope();
            // 从工厂中获取一个配置好了的租户信息的数据库上下文
            builder.Register<SqlDbContext>(p => p.Resolve<SqlDbContextFactory>().CreateDbContext()).InstancePerLifetimeScope();
            // 多租户dao层注入         
            base.Load(builder);
        }

        /// <summary>
        /// 通过程序集名称加载对应程序集到内存中
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private Assembly GetAssembly(string assemblyName)
        {
            return AssemblyLoadContext.
                Default.
                LoadFromAssemblyPath(AppContext.BaseDirectory + $"{assemblyName}.dll");
        }
    }
}
