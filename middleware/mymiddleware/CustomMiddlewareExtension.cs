using Microsoft.AspNetCore.Builder;

namespace middleware.mymiddleware
{
    /// <summary>
    /// 封装中间件的扩展类
    /// </summary>
    public static class CustomMiddlewareExtension
    {
        /// <summary>
        /// 添加HttpMethodCheckMiddleware中间件的扩展方法
        /// </summary>
        public static IApplicationBuilder UseHttpMethodCheckMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HttpMethodCheckMiddleware>();
        }
    }
}