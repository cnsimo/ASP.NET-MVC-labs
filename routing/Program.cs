
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace routing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<MVCRouteStartup>();
                //builder.UseStartup<Startup>();
            }).Build().Run();
        }
    }
}