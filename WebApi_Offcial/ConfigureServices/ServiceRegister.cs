using Autofac;
using DataSphere.ES;
using IDataSphere.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Nest;
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
        private const string SERVICE_ASSEMBLY_NAME = "Service";
        private const string DAO_ASSEMBLY_NAME = "DataSphere";
        private const string WEBAPI_ASSEMBLY_NAME = "WebApi_Offcial";

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            // WebAPI层注入AOP切面
            builder.RegisterAssemblyTypes(GetAssembly(WEBAPI_ASSEMBLY_NAME))
                   .Where(p => p.Name.EndsWith("ActionFilter"))
                   .InstancePerLifetimeScope();

            // Service层业务类注入
            builder.RegisterAssemblyTypes(GetAssembly(SERVICE_ASSEMBLY_NAME))
                   .Where(p => p.Name.EndsWith("ServiceImpl"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            // DataSphere层注入数据库访问类
            builder.RegisterAssemblyTypes(GetAssembly(DAO_ASSEMBLY_NAME))
                   .Where(p => p.Name.EndsWith("Dao"))
                   .AsImplementedInterfaces()

                   .InstancePerLifetimeScope();

            // DataSphere层注入ES链接
            builder.RegisterAssemblyTypes(GetAssembly(DAO_ASSEMBLY_NAME))
                   .Where(p => p.Name.EndsWith("Repository") && p.Namespace.EndsWith("ES"))
                   .InstancePerLifetimeScope();

            // 注入ES链接
            builder.RegisterType<ElasticSearchHelper>().AsImplementedInterfaces().InstancePerLifetimeScope();

            // 注入Http上下文
            builder.RegisterType<HttpContextAccessor>().InstancePerLifetimeScope();

            // 注入DbContext
            RegisterDbContext(builder);
            RegisterEsClent(builder);

            base.Load(builder);
        }

        /// <summary>
        /// 通过程序集名称加载对应程序集到内存中
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private Assembly GetAssembly(string assemblyName)
        {
            return AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + $"{assemblyName}.dll");
        }

        /// <summary>
        /// 注入DbContext
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        private ContainerBuilder RegisterDbContext(ContainerBuilder builder)
        {
            builder.Register<UserProvider>(p =>
            {
                // 每次请求序列化一个新的附加有当前用户信息的类
                HttpContextAccessor _httpContextAccessor = p.Resolve<HttpContextAccessor>();
                long.TryParse(_httpContextAccessor?.HttpContext?.User.FindFirst(ClaimsUserConst.USER_ID)?.Value, out long userId);
                bool.TryParse(_httpContextAccessor?.HttpContext?.User.FindFirst(ClaimsUserConst.IS_REMEMBER)?.Value, out bool isSuperManage);
                string roleIds = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimsUserConst.ROLE_IDs)?.Value.ToString();           
                return new UserProvider(userId, roleIds, isSuperManage);
            }).InstancePerLifetimeScope();


            // 注册数据库上下文实例化工厂
            builder.RegisterType<SqlDbContextFactory>().InstancePerLifetimeScope();
            // 从工厂中获取一个配置好了的租户信息的数据库上下文           
            builder.Register<SqlDbContext>(p => p.Resolve<SqlDbContextFactory>().CreateDbContext()).InstancePerLifetimeScope();
            return builder;
        }

        /// <summary>
        /// 注册ES链接
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        private ContainerBuilder RegisterEsClent(ContainerBuilder builder)
        {
            // 注册ElasticSearchHelper
            builder.RegisterType<ElasticSearchHelper>().SingleInstance();
            // 从ElasticSearchHelper获取一个已经配置好的单例链接
            builder.Register<ElasticClient>(p => p.Resolve<ElasticSearchHelper>().GetClient()).SingleInstance();
            return builder;
        }
    }
}
