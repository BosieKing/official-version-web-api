namespace WebApi_Offcial.MiddleWares
{
    /// <summary>
    /// 日志管道
    /// </summary>
    public static class ActionHistoryMiddlewareExtensions
    {
        /// <summary>
        /// 添加日志管道中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UserActionHistoryMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ActionHistoryMiddleware>();
            return builder;
        }
    }
}
