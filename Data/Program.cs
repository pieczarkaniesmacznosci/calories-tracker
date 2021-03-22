using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Data.Extensions;
using Data.Web.DbContexts;
using Microsoft.AspNetCore;

namespace Data
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>  
            WebHost.CreateDefaultBuilder(args)  
                .UseStartup<Startup>();  
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
