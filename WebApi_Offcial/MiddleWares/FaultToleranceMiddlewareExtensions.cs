namespace WebApi_Offcial.MiddleWares
{
    /// <summary>
    /// 容错中间件拓展类
    /// </summary>
    public static class FaultToleranceMiddlewareExtensions
    {
        /// <summary>
        /// 添加自定义容错中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UserFaultToleranceMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<FaultToleranceMiddleware>();
            return builder;
        }
    }
}
