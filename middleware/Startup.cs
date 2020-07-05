using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using middleware.mymiddleware;

namespace middleware
{
    public class Startup
    {
        /// <summary>
        /// 注册应用程序所需的服务
        /// </summary>
        public void ConfigureServices(IServiceCollection service)
        {
            
        }

        /// <summary>
        /// 注册管道中间件
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            // 开发环境，添加开发者异常页面
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 使用自定义的中间件
            app.UseHttpMethodCheckMiddleware();

            // Use 方式
            app.Use(async (context, next) =>
            {
                if(context.Request.Path == new PathString("/use"))
                {
                    await context.Response.WriteAsync($"Path: {context.Request.Path}");
                }
                await next();
            });

            // UseWhen 方式
            app.UseWhen(context => context.Request.Path == new PathString("/usewhen"),
            a => a.Use(async (context, next) =>
            {
                await context.Response.WriteAsync($"Path: {context.Request.Path}");
                await next();
            }));

            // Map 方式
            app.Map(new PathString("/map"),
            a => a.Use(async (context, next) =>
            {
                // context.request.path 获取不到正确的路径
                //await context.Response.WriteAsync($"Path: {context.Request.Path}");
                await context.Response.WriteAsync($"PathBase: {context.Request.PathBase}");
                foreach(var item in context.Request.Headers)
                {
                    await context.Response.WriteAsync($"\n{item.Key}: {item.Value}");
                }
            }));

            // MapWhen 方式
            app.MapWhen(context => context.Request.Path == new PathString("/mapwhen"),
            a => a.Use(async (context, next) =>
            {
                await context.Response.WriteAsync($"Path: {context.Request.Path}");
                await next();
            }));

            // Run 放在最后，可有可无，主要为了验证是否可以回到原来的管道上继续执行
            app.Run(async (context)=>
            {
                await context.Response.WriteAsync("\nCongratulation, return to the original pipe.");
            });
        }
    }
}
