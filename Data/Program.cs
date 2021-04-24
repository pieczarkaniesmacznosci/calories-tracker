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
            CreateWebHostBuilder(args).Build().MigrateDatabase<CaloriesLibraryContext>().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>  
            WebHost.CreateDefaultBuilder(args)  
                .UseStartup<Startup>();
    }
}
