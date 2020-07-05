using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace routing
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection servcies)
        {

        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 新建一个路由处理器
            var trackPackageRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync($"Hello! Route values: {string.Join(", ", routeValues)}");
            });
            var routeBuilder = new RouteBuilder(app, trackPackageRouteHandler);
            // 通过MapRoute添加路由模板
            routeBuilder.MapRoute("Track Package Route", "package/{opration=query}/{id:int=1}");
            routeBuilder.MapGet("hello/{name}", context =>
            {
                var name = context.GetRouteValue("name");
                return context.Response.WriteAsync($"Hi, {name}!");
            });
            var routes = routeBuilder.Build();
            app.UseRouter(routes);
        }
    }
}