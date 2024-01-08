using Autofac;
using IDataSphere.DatabaseContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Model.Commons.Domain;
using Newtonsoft.Json;
using System.Globalization;
using UtilityToolkit.Helpers;
using UtilityToolkit.Tools;
using WebApi_Offcial.ConfigureServices;
using WebApi_Offcial.MiddleWares;
using Yitter.IdGenerator;

var builder = WebApplication.CreateBuilder(args);
// 业务配置
builder.Configuration.AddJsonFile("serversettings.json");

// 冷配置
builder.Configuration.AddConfigSettingBind();

// 注入http服务访问
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

// 注入控制器服务
// 增加全局权限过滤器
// 格式化输出
builder.Services.AddControllers(option =>
{
    // 全局权限过滤器
    option.Filters.Add(new AuthorizeFilter());
})
                .AddDataAnnotationsLocalization(option =>
                {
                    // 模型注释多语言
                    option.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(UserTips));
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    // 友好化模型验证失败
                    options.InvalidModelStateResponseFactory = (context) =>
                    {
                        var error = context.ModelState;
                        return new JsonResult(DataResponseModel.IsFailure("模型验证失败"));
                    };
                })
                .AddNewtonsoftJson(p =>
                {
                    // 输出的时间格式化
                    p.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    // 忽略循环引用
                    p.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

// 依赖注入，替换原始容器。引入自定义依赖注入配置
builder.Services.AddAutofacMultitenantRequestServices();
// 配置多租户业务支持
builder.Host.UseServiceProviderFactory(new AutofacMultitenantServiceProviderFactory(MultitenantServiceRegister.ConfigureMultitenantContainer));
// 注入服务
builder.Host.ConfigureContainer<ContainerBuilder>(p =>
{
    p.RegisterModule<ServiceRegister>();
});

// 注入雪花函数
YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = 1 });

// Jwt认证
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddScheme<JwtBearerOptions, JwtHandler>(JwtBearerDefaults.AuthenticationScheme, null);


// 注入最小API服务，为了在Swagger中展示
builder.Services.AddEndpointsApiExplorer();

// 注入Swagger服务
builder.Services.AddSwaggerDoc();

// 替换为Log4Net日志
builder.Logging.AddLog4Net("ConfigFiles/Log4net.config");

// 添加本地化服务
builder.Services.AddLocalization();

// 修改模型验证返回格式
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // 修改模型验证返回格式
    options.InvalidModelStateResponseFactory = (context) =>
    {
        var errorMsgs = context.ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage)).ToArray();
        return new JsonResult(DataResponseModel.IsFailure(String.Join(",", errorMsgs)));
    };
});

//  和swagger一起搭配使用会在左上角出现请求耗时分析哦！
builder.Services.AddMiniProfiler(option =>
{
    //  配置基础路由路径
    option.RouteBasePath = "/profiler";
});
var app = builder.Build();

// 启用本地化支持
var languages = app.Configuration.GetSection("UserTipsConfig").GetChildren().Select(p => new CultureInfo(p.Value)).ToArray();
app.UseRequestLocalization(new RequestLocalizationOptions
{
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

// 判断是否为开发环境

// 开发环境启用Swagger
app.UseSwagger();
app.UseSwaggerUIOption();

// 启用终结点匹配
app.UseEndpoints(o => o.MapControllers());

// 设置终结点
app.Run();
