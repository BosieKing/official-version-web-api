﻿using log4net;
using Model.Commons.Domain;
using Model.Repositotys.Log;
using UtilityToolkit.Helpers;
using UtilityToolkit.Utils;

namespace WebApi_Offcial.MiddleWares
{
    /// <summary>
    /// 自定义容错中间件
    /// </summary>
    public class FaultToleranceMiddleware
    {
        /// <summary>
        /// 请求委托
        /// </summary>
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
        /// 返回错误信息友好化
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
            TL_ErrorLog log = new();
            log.ExceptionType = exception.GetType().Name;
            log.ExceptionSource = exception?.Source;
            log.ExceptionMessage = exception?.Message;
            log.InnerExceptionMessage = exception?.InnerException?.Message;
            QueueSingletonHelper<TL_ErrorLog>.Instance.Add(log);
            await context.Response.WriteAsJsonAsync(ServiceResult.IsFailure(exception.Message)).ConfigureAwait(false);
            // 管道短路
            return;
        }
    }
}
