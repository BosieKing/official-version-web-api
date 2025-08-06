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
using Quartz;
using System.Globalization;
using UtilityToolkit.Helpers;
using UtilityToolkit.Tools;
using WebApi_Offcial.ConfigureServices;
using WebApi_Offcial.MiddleWares;
using Yitter.IdGenerator;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("serversettings.json");


builder.Configuration.AddConfigSettingBind();

builder.Services.AddHttpContextAccessor();


builder.Services.AddPooledDbContextFactory<SqlDbContext>(options =>
{

    options.UseMySql(
        ConfigSettingTool.ConnectionConfigOptions.DefaultConnectionStr,
        ServerVersion.AutoDetect(ConfigSettingTool.ConnectionConfigOptions.DefaultConnectionStr)
    );
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


builder.Services.AddResponseCompression();


builder.Services.AddHttpClient();


builder.Services.AddSignalRCore();

builder.Services.AddControllers(option => option.Filters.Add(new AuthorizeFilter()))
.AddDataAnnotationsLocalization(option =>
{

    option.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(UserTips));
})
.AddNewtonsoftJson(p =>
{

    p.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

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


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());



builder.Host.ConfigureContainer<ContainerBuilder>(p =>
{
    p.RegisterModule<ServiceRegister>();
});


YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = 1 });


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddScheme<JwtBearerOptions, JwtHandler>(JwtBearerDefaults.AuthenticationScheme, null);


builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerDoc();


builder.Logging.AddLog4Net("ConfigFiles/Log4net.config");


builder.Services.AddLocalization();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{

    options.InvalidModelStateResponseFactory = (context) =>
    {
        var errorMsgs = context.ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage)).ToArray();
        return new JsonResult(ServiceResult.Fail(String.Join(",", errorMsgs)));
    };
});

// Program.cs
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

builder.Services.AddMiniProfiler(option =>
{

    option.RouteBasePath = "/profiler";
});


builder.Services.AddHostedService<HostService>();


builder.Services.AddCustomizeQuartz();

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


app.UseSwagger();
app.UseSwaggerUIOption();


app.UseEndpoints(o => o.MapControllers());


app.Run();
