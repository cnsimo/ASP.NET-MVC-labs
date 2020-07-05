using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace middleware.mymiddleware
{
    /// <summary>
    /// 请求方法检查中间件，仅处理HEAD和GET方法
    /// </summary>
    public class HttpMethodCheckMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 构造方法，必须有的
        /// </summary>
        /// <param name="requestDelegate">下一个中间件</param>
        public HttpMethodCheckMiddleware(RequestDelegate requestDelegate)
        {
            this._next = requestDelegate;
        }

        /// <summary>
        /// 中间件调度方法
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>TASK任务状态</returns>
        public Task Invoke(HttpContext context)
        {
            // 如果符合条件，则将httpcontext传给下一个中间件处理
            if(context.Request.Method.ToUpper().Equals(HttpMethods.Head)
                || context.Request.Method.ToUpper().Equals(HttpMethods.Get))
            {
                return _next(context);
            }

            // 否则直接返回处理完成
            context.Response.StatusCode = 400;
            context.Response.Headers.Add("X-AllowedHTTPVerb", new[] {"GET,HEAD"});
            context.Response.ContentType = "text/plain;charset=utf-8";  // 防止中文乱码
            context.Response.WriteAsync("只支持GET、HEAD方法");
            return Task.CompletedTask;
        }
    }
}