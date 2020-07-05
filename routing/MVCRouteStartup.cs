using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace routing
{
    public class MVCRouteStartup
    {
        public void ConfigureServices(IServiceCollection service)
        {
            service.AddControllersWithViews();
            service.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(name: "MVC TEST ROUTE", 
            //                     template: "{controller}/{action}/{id?}", 
            //                     defaults: new {controller = "Home", action = "Index"});
            // });
            app.UseRouting();
            app.UseEndpoints(endpoionts =>
            {
                endpoionts.MapControllerRoute(name: "MVC TEST ROUTE", 
                                            pattern: "{controller}/{action}/{id?}",
                                            defaults: new {controller = "Home", action = "Index"});
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}