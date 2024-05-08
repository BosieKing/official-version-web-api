namespace WebApi_Offcial.MiddleWares
{
    /// <summary>
    /// 日志操作中间件
    /// </summary>
    public class ActionHistoryMiddleware
    {
        private RequestDelegate _next;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public ActionHistoryMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        /// <summary>
        /// 中间件处理方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // 执行前
            ActionExecuting(context);
            try
            {
                // 传递给下一个中间件
                await _next(context);
            }
            catch (Exception)
            {
                // 管道短路
                return;
            }
            finally
            {
                // 执行后
                ActionExecuted(context);
            }
        }

        /// <summary>
        /// 方法执行前
        /// </summary>
        /// <param name="context"></param>
        private void ActionExecuting(HttpContext context)
        {

            // 注入请求信息到调度器

        }

        /// <summary>
        /// 方法执行后
        /// </summary>
        /// <param name="context"></param>
        private void ActionExecuted(HttpContext context)
        {

            // 注入报错
        }
    }
}
