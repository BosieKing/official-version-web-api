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
// ҵ������
builder.Configuration.AddJsonFile("serversettings.json");

// ������
builder.Configuration.AddConfigSettingBind();

// ע��http�������
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

// ע�����������
// ����ȫ��Ȩ�޹�����
// ��ʽ�����
builder.Services.AddControllers(option =>
{
    // ȫ��Ȩ�޹�����
    option.Filters.Add(new AuthorizeFilter());
})
                .AddDataAnnotationsLocalization(option =>
                {
                    // ģ��ע�Ͷ�����
                    option.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(UserTips));
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    // �Ѻû�ģ����֤ʧ��
                    options.InvalidModelStateResponseFactory = (context) =>
                    {
                        var error = context.ModelState;
                        return new JsonResult(DataResponseModel.IsFailure("ģ����֤ʧ��"));
                    };
                })
                .AddNewtonsoftJson(p =>
                {
                    // �����ʱ���ʽ��
                    p.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    // ����ѭ������
                    p.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

// ����ע�룬�滻ԭʼ�����������Զ�������ע������
builder.Services.AddAutofacMultitenantRequestServices();
// ���ö��⻧ҵ��֧��
builder.Host.UseServiceProviderFactory(new AutofacMultitenantServiceProviderFactory(MultitenantServiceRegister.ConfigureMultitenantContainer));
// ע�����
builder.Host.ConfigureContainer<ContainerBuilder>(p =>
{
    p.RegisterModule<ServiceRegister>();
});

// ע��ѩ������
YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = 1 });

// Jwt��֤
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddScheme<JwtBearerOptions, JwtHandler>(JwtBearerDefaults.AuthenticationScheme, null);


// ע����СAPI����Ϊ����Swagger��չʾ
builder.Services.AddEndpointsApiExplorer();

// ע��Swagger����
builder.Services.AddSwaggerDoc();

// �滻ΪLog4Net��־
builder.Logging.AddLog4Net("ConfigFiles/Log4net.config");

// ��ӱ��ػ�����
builder.Services.AddLocalization();

// �޸�ģ����֤���ظ�ʽ
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // �޸�ģ����֤���ظ�ʽ
    options.InvalidModelStateResponseFactory = (context) =>
    {
        var errorMsgs = context.ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage)).ToArray();
        return new JsonResult(DataResponseModel.IsFailure(String.Join(",", errorMsgs)));
    };
});

//  ��swaggerһ�����ʹ�û������Ͻǳ��������ʱ����Ŷ��
builder.Services.AddMiniProfiler(option =>
{
    //  ���û���·��·��
    option.RouteBasePath = "/profiler";
});
var app = builder.Build();

// ���ñ��ػ�֧��
var languages = app.Configuration.GetSection("UserTipsConfig").GetChildren().Select(p => new CultureInfo(p.Value)).ToArray();
app.UseRequestLocalization(new RequestLocalizationOptions
{
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

// �ж��Ƿ�Ϊ��������

// ������������Swagger
app.UseSwagger();
app.UseSwaggerUIOption();

// �����ս��ƥ��
app.UseEndpoints(o => o.MapControllers());

// �����ս��
app.Run();
