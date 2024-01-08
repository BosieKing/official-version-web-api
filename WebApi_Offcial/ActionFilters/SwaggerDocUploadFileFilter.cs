using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace WebApi_Offcial.ActionFilters
{
    /// <summary>
    /// Swagger文件上传参数修正
    /// </summary>
    public class SwaggerDocUploadFileFilter : IOperationFilter
    {
        /// <summary>
        /// 修正
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // 获取参数为IFormFile的方法
            var actionList = context.ApiDescription.ActionDescriptor.Parameters.Where(p => p.ParameterType == typeof(IFormFile)).ToList();
            if (actionList.Count > 0)
            {
                foreach (var item in actionList)
                {
                    // 定义一个新的模型
                    Dictionary<string, OpenApiSchema> schema = new Dictionary<string, OpenApiSchema>();
                    // 获取参数名称
                    string parametersName = item.Name;
                    // 修改这个参数的上传内容为binary
                    schema[parametersName] = new OpenApiSchema { Description = "文件上传服务", Type = "string", Format = "binary" };
                    // 增加一个文件描述。指定这个文件类型使用表单提交
                    Dictionary<string, OpenApiMediaType> content = new Dictionary<string, OpenApiMediaType>();
                    content["multipart/form-data"] = new OpenApiMediaType { Schema = new OpenApiSchema { Type = "object", Properties = schema } };
                    // 替换请求正文表述
                    operation.RequestBody = new OpenApiRequestBody() { Content = content };
                }
            }
        }
    }
}
