using log4net;
using Model.Commons.Domain;
using UtilityToolkit.Utils;

namespace WebApi_Offcial.MiddleWares
{
    /// <summary>
    /// 自定义容错中间件
    /// </summary>
    public class FaultToleranceMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILog log = LogManager.GetLogger("ErrorLog");
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public FaultToleranceMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 中间件方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                log.Error(new
                {
                    ErrorMsg =
                    $"Source:{ex.Source}" +
                    $"Meg:{ex.Message}" +
                    $"helpelink:{ex.HelpLink}" +
                    $"Hrreuslt:{ex.HResult}"
                }.ToJson());
                await WriterExceptionMessageAsync(ex, context).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 友好返回错误信息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task WriterExceptionMessageAsync(Exception exception, HttpContext context)
        {
            if (exception == null)
            {
                return;
            }
            context.Response.ContentType = context.Request.Headers["Accept"];
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(ServiceResult.IsFailure(exception.Message)).ConfigureAwait(false);
            // 管道短路
            return;
        }
    }
}
