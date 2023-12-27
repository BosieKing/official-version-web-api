using Microsoft.OpenApi.Models;
using SharedLibrary.Enums;
using Swashbuckle.AspNetCore.SwaggerUI;
using UtilityToolkit.Utils;
using WebApi_Offcial.ActionFilters;

namespace WebApi_Offcial.ConfigureServices
{
    /// <summary>
    /// Swagger文档自定义封装服务
    /// </summary>
    public static class SwaggerDocExtensions
    {
        /// <summary>
        /// 自定义配置文档信息
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                // 创造文档
                string[] groupList = typeof(SwaggerGroupEnum).GetKeyList();
                var groupDic = typeof(SwaggerGroupEnum).TryParseDic();
                foreach (var p in groupList)
                {
                    // 声明swagger文档版本内容
                    options.SwaggerDoc(p, new OpenApiInfo
                    {
                        // 版本
                        Version = "V1",
                        // 标题
                        Title = groupDic[p],
                        // 描述
                        Description = "我的接口文档",
                        Contact = new OpenApiContact
                        {
                            Name = "Bosie",
                            Email = "487378523@qq.com",
                            Url = null
                        }
                    });
                }

                // 声明一个认证方式
                options.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme
                {
                    Description = "认证",
                    // 参数名称
                    Name = "Authorization",
                    // 放在请求头中
                    In = ParameterLocation.Header,
                    // 类型
                    Type = SecuritySchemeType.Http,
                    // Schema指的是接口参数和返回值等的结构信息。
                    Scheme = "bearer"

                });
                // 声明一个组合，指定这个组合与上方认证方式搭配                
                var scheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "JwtBearer" }
                };

                // 注册请求的时候必须使用该验证方式
                options.AddSecurityRequirement(new OpenApiSecurityRequirement { [scheme] = new string[0] });

                // 分组下拉回调
                options.DocInclusionPredicate((docName, apiDescription) =>
                {
                    if (apiDescription.HttpMethod == null)
                    {
                        // 没有包含任何Http请求方法不在文档展示
                        return false;
                    }
                    // 是对应分组的方法返回
                    return apiDescription.GroupName == docName;
                });

                // 按请求类型排序
                options.OrderActionsBy(p => p.HttpMethod);
                // 加载写的注释
                string filePath = Path.Combine(AppContext.BaseDirectory, "WebApi_Offcial.xml");
                options.IncludeXmlComments(filePath, true);
                // 文件参数修正
                options.OperationFilter<SwaggerDocUploadFileFilter>();
            });
        }

        /// <summary>
        /// 配置UI
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerUIOption(this IApplicationBuilder app)
        {
            return app.UseSwaggerUI(options =>
            {
                var groupList = typeof(SwaggerGroupEnum).GetKeyList();
                foreach (var p in groupList)
                {
                    // 默认不展开
                    options.DocExpansion(DocExpansion.None);
                    // 不显示模组
                    options.DefaultModelsExpandDepth(-1);
                    // 获取文档列表
                    options.SwaggerEndpoint($"/swagger/{p}/swagger.json", $"{p}");
                }
            });
        }
    }
}
