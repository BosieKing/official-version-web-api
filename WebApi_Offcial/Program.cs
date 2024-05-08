using Autofac;
using IDataSphere.DatabaseContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Model.Commons.Domain;
using Newtonsoft.Json;
using Quartz;
using System.Globalization;
using UtilityToolkit.Helpers;
using UtilityToolkit.Tools;
using WebApi_Offcial.ConfigureServices;
using WebApi_Offcial.MiddleWares;
using Yitter.IdGenerator;

var builder = WebApplication.CreateBuilder(args);
// 注入业务配置
builder.Configuration.AddJsonFile("serversettings.json");

// 模型绑定
builder.Configuration.AddConfigSettingBind();

// 注入Http服务访问
builder.Services.AddHttpContextAccessor();

// 注入数据库池服务
builder.Services.AddPooledDbContextFactory<SqlDbContext>(o =>
{
    o.UseSqlServer(ConfigSettingTool.ConnectionConfigOptions.DefaultConnectionStr);
    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// 注入响应式压缩服务
builder.Services.AddResponseCompression();

// 注入HttpClientFactory
builder.Services.AddHttpClient();

// AddSignalRCore比AddSignalR多调用
builder.Services.AddSignalRCore();

// 注入控制器服务
// 全部的控制器增加鉴权过滤器
// 格式化输出
builder.Services.AddControllers(option => option.Filters.Add(new AuthorizeFilter()))
.AddDataAnnotationsLocalization(option =>
{
    // 提供多语言模板
    option.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(UserTips));
})
.AddNewtonsoftJson(p =>
{
    // 输出的时间格式化
    p.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    // 忽略循环引用
    p.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
})
.ConfigureApiBehaviorOptions(options =>
{
    // 友好化模型验证失败
    options.InvalidModelStateResponseFactory = (context) =>
    {
        var error = context.ModelState;
        return new JsonResult(ServiceResult.IsFailure("模型验证失败"));
    };
});

// AutoFac替换原始容器
builder.Services.AddAutofacMultitenantRequestServices();

// 配置多租户业务支持服务
builder.Host.UseServiceProviderFactory(new AutofacMultitenantServiceProviderFactory(MultitenantServiceRegister.ConfigureMultitenantContainer));

// AutoFac服务注入
builder.Host.ConfigureContainer<ContainerBuilder>(p =>
{
    p.RegisterModule<ServiceRegister>();
});

// 注入分布式雪花函数
YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = 1 });

// 注入Jwt认证服务
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddScheme<JwtBearerOptions, JwtHandler>(JwtBearerDefaults.AuthenticationScheme, null);

// 注入最小API服务，为了在Swagger中展示
builder.Services.AddEndpointsApiExplorer();

// 注入Swagger文档服务
builder.Services.AddSwaggerDoc();

// 替换为Log4Net日志
builder.Logging.AddLog4Net("ConfigFiles/Log4net.config");

// 添加本地化多语言服务
builder.Services.AddLocalization();

// 修改模型验证返回格式
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // 修改模型验证返回格式
    options.InvalidModelStateResponseFactory = (context) =>
    {
        var errorMsgs = context.ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage)).ToArray();
        return new JsonResult(ServiceResult.IsFailure(String.Join(",", errorMsgs)));
    };
});

// 注入请求分析服务
builder.Services.AddMiniProfiler(option =>
{
    //  配置基础路由路径
    option.RouteBasePath = "/profiler";
});

// 注入定时任务
builder.Services.AddCustomizeQuartz();

var app = builder.Build();

// 启用本地化支持
// 从配置文件中获取可支持的语言
CultureInfo[] languages = app.Configuration.GetSection("UserTipsConfig").GetChildren().Select(p => new CultureInfo(p.Value)).ToArray();
app.UseRequestLocalization(new RequestLocalizationOptions
{
    // 默认语言为zh-CN
    DefaultRequestCulture = new RequestCulture(culture: languages[0], uiCulture: languages[0]),
    SupportedCultures = languages,
    SupportedUICultures = languages
});

// 启用容错中间件
app.UserFaultToleranceMiddleware();

// 启用服务器缓存
app.UseResponseCaching();

// 启用响应压缩
app.UseResponseCompression();

// 启用跨域
app.UseCors("WebAPI");

// 映射属性路由控制器  
app.MapControllers();

// 启用路由匹配
app.UseRouting();

// 启用鉴权中间件
app.UseAuthentication();

// 启用授权中间件
app.UseAuthorization();

// 启用Swagger
app.UseSwagger();
app.UseSwaggerUIOption();

// 启用终结点匹配
app.UseEndpoints(o => o.MapControllers());

// 设置终结点
app.Run();
