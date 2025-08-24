using Autofac;
using Autofac.Extensions.DependencyInjection;
using IDataSphere.DatabaseContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Model.Commons.Domain;
using Newtonsoft.Json;
using OnceMi.AspNetCore.OSS;
using Quartz;
using RabbitMQ.Client;
using System.Globalization;
using UtilityToolkit.Helpers;
using UtilityToolkit.Tools;
using WebApi_Offcial.ConfigureServices;
using WebApi_Offcial.MiddleWares;
using Yitter.IdGenerator;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("serversettings.json");

// 对配置文件进行绑定到实体类
builder.Configuration.AddConfigSettingBind();

// 注入http服务
builder.Services.AddHttpContextAccessor();

// 注入数据库连接
builder.Services.AddPooledDbContextFactory<SqlDbContext>(options =>
{

    options.UseMySql(
        ConfigSettingTool.ConnectionConfigOptions.DefaultConnectionStr,
        ServerVersion.AutoDetect(ConfigSettingTool.ConnectionConfigOptions.DefaultConnectionStr)
    );
    // 默认所有查询都是非跟踪查询
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// 增加响应压缩服务
builder.Services.AddResponseCompression();

// 注入http请求支持
builder.Services.AddHttpClient();

// 注入signalrf服务
builder.Services.AddSignalRCore();

// 为控制器注入服务，并给所有控制器加上需要鉴权的特性
builder.Services.AddControllers(option => option.Filters.Add(new AuthorizeFilter()))
.AddDataAnnotationsLocalization(option =>
{
    // 增加多语言支持
    option.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(UserTips));
})
.AddNewtonsoftJson(p =>
{
    // 序列化对于datetime特殊处理
    p.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

    // 无视循环引用
    p.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
})
.ConfigureApiBehaviorOptions(options =>
{

    options.InvalidModelStateResponseFactory = (context) =>
    {
        var error = context.ModelState;
        return new JsonResult(ServiceResult.Fail("参数错误"));
    };
});

// 替换成为autofacDI容器
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


// autofac的依赖注入
builder.Host.ConfigureContainer<ContainerBuilder>(p =>
{
    p.RegisterModule<ServiceRegister>();
});

// 设置雪花生成器的开始值
YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = 1 });


// 增加鉴权服务
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddScheme<JwtBearerOptions, JwtHandler>(JwtBearerDefaults.AuthenticationScheme, null);

// 用于生成 API 文档。它注册了一些服务，这些服务被 Swagger 和其他 API 文档工具使用，
builder.Services.AddEndpointsApiExplorer();

// 注入swagger文档支持服务
builder.Services.AddSwaggerDoc();

// 配置log4net配置文件
builder.Logging.AddLog4Net("ConfigFiles/Log4net.config");

// 增加多语言服务
builder.Services.AddLocalization();

// 修改模型验证错误返回结果
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = (context) =>
    {
        var errorMsgs = context.ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage)).ToArray();
        return new JsonResult(ServiceResult.Fail(String.Join(",", errorMsgs)));
    };
});

// 设置跨域策略
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // 允许所有来源（生产环境应限制）
              .AllowAnyMethod()  // 允许所有 HTTP 方法
              .AllowAnyHeader()  // 允许所有头（包括 Authorization）
              .SetPreflightMaxAge(TimeSpan.FromMinutes(10)); // 缓存 OPTIONS 结果
    });
});

// 注入请求分析服务
builder.Services.AddMiniProfiler(option =>
{

    option.RouteBasePath = "/profiler";
});

// 注入主机服务，开始定时任务
builder.Services.AddHostedService<HostService>();

// 注入Quartz服务
builder.Services.AddCustomizeQuartz();

// 对RabbitMQ服务进行单例注册
builder.Services.AddSingleton<RabbitMQHelper>();
builder.Services.AddSingleton<IConnection>(provider => provider.GetRequiredService<RabbitMQHelper>().GetConnection());

// 注入minio服务
builder.Services.AddOSSService(ConfigSettingTool.MinIOConfig.DefaultKey, "MinIOConfig");
builder.Services.AddScoped<MinIOStrategyHelper>();




var app = builder.Build();


CultureInfo[] languages = app.Configuration.GetSection("UserTipsConfig").GetChildren().Select(p => new CultureInfo(p.Value)).ToArray();
app.UseRequestLocalization(new RequestLocalizationOptions
{

    DefaultRequestCulture = new RequestCulture(culture: languages[0], uiCulture: languages[0]),
    SupportedCultures = languages,
    SupportedUICultures = languages
});

app.UseCors("AllowAll");

app.UseStaticFiles();



app.UseMiniProfiler();

app.UserFaultToleranceMiddleware();


app.UseResponseCaching();


app.UseResponseCompression();


app.UseCors("AllowAll");


app.MapControllers();


app.UseRouting();


app.UseAuthentication();


app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUIOption();
}
app.UseEndpoints(o => o.MapControllers());


app.Run();
