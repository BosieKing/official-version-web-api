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
// ע��ҵ������
builder.Configuration.AddJsonFile("serversettings.json");

// ģ�Ͱ�
builder.Configuration.AddConfigSettingBind();

// ע��Http�������
builder.Services.AddHttpContextAccessor();

// ע�����ݿ�ط���
builder.Services.AddPooledDbContextFactory<SqlDbContext>(o =>
{
    o.UseSqlServer(ConfigSettingTool.ConnectionConfigOptions.DefaultConnectionStr);
    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// ע����Ӧʽѹ������
builder.Services.AddResponseCompression();

// ע��HttpClientFactory
builder.Services.AddHttpClient();

// AddSignalRCore��AddSignalR�����
builder.Services.AddSignalRCore();

// ע�����������
// ȫ���Ŀ��������Ӽ�Ȩ������
// ��ʽ�����
builder.Services.AddControllers(option => option.Filters.Add(new AuthorizeFilter()))
.AddDataAnnotationsLocalization(option =>
{
    // �ṩ������ģ��
    option.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(UserTips));
})
.AddNewtonsoftJson(p =>
{
    // �����ʱ���ʽ��
    p.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    // ����ѭ������
    p.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
})
.ConfigureApiBehaviorOptions(options =>
{
    // �Ѻû�ģ����֤ʧ��
    options.InvalidModelStateResponseFactory = (context) =>
    {
        var error = context.ModelState;
        return new JsonResult(ServiceResult.IsFailure("ģ����֤ʧ��"));
    };
});

// AutoFac�滻ԭʼ����
builder.Services.AddAutofacMultitenantRequestServices();

// ���ö��⻧ҵ��֧�ַ���
builder.Host.UseServiceProviderFactory(new AutofacMultitenantServiceProviderFactory(MultitenantServiceRegister.ConfigureMultitenantContainer));

// AutoFac����ע��
builder.Host.ConfigureContainer<ContainerBuilder>(p =>
{
    p.RegisterModule<ServiceRegister>();
});

// ע��ֲ�ʽѩ������
YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = 1 });

// ע��Jwt��֤����
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddScheme<JwtBearerOptions, JwtHandler>(JwtBearerDefaults.AuthenticationScheme, null);

// ע����СAPI����Ϊ����Swagger��չʾ
builder.Services.AddEndpointsApiExplorer();

// ע��Swagger�ĵ�����
builder.Services.AddSwaggerDoc();

// �滻ΪLog4Net��־
builder.Logging.AddLog4Net("ConfigFiles/Log4net.config");

// ��ӱ��ػ������Է���
builder.Services.AddLocalization();

// �޸�ģ����֤���ظ�ʽ
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // �޸�ģ����֤���ظ�ʽ
    options.InvalidModelStateResponseFactory = (context) =>
    {
        var errorMsgs = context.ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage)).ToArray();
        return new JsonResult(ServiceResult.IsFailure(String.Join(",", errorMsgs)));
    };
});

// ע�������������
builder.Services.AddMiniProfiler(option =>
{
    //  ���û���·��·��
    option.RouteBasePath = "/profiler";
});

// ע�붨ʱ����
builder.Services.AddCustomizeQuartz();

var app = builder.Build();

// ���ñ��ػ�֧��
// �������ļ��л�ȡ��֧�ֵ�����
CultureInfo[] languages = app.Configuration.GetSection("UserTipsConfig").GetChildren().Select(p => new CultureInfo(p.Value)).ToArray();
app.UseRequestLocalization(new RequestLocalizationOptions
{
    // Ĭ������Ϊzh-CN
    DefaultRequestCulture = new RequestCulture(culture: languages[0], uiCulture: languages[0]),
    SupportedCultures = languages,
    SupportedUICultures = languages
});

// �����ݴ��м��
app.UserFaultToleranceMiddleware();

// ���÷���������
app.UseResponseCaching();

// ������Ӧѹ��
app.UseResponseCompression();

// ���ÿ���
app.UseCors("WebAPI");

// ӳ������·�ɿ�����  
app.MapControllers();

// ����·��ƥ��
app.UseRouting();

// ���ü�Ȩ�м��
app.UseAuthentication();

// ������Ȩ�м��
app.UseAuthorization();

// ����Swagger
app.UseSwagger();
app.UseSwaggerUIOption();

// �����ս��ƥ��
app.UseEndpoints(o => o.MapControllers());

// �����ս��
app.Run();
