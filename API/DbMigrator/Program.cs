using Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DbMigrator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(cfg =>
                {
                    cfg.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<CaloriesLibraryContext>(options =>
                    {
                        var rawConnectionString = new StringBuilder(context.Configuration.GetConnectionString("SqlServer"));
                        var connectionString = rawConnectionString
                            .Replace("ENVID", context.Configuration["DB_UID"])
                            .Replace("ENVDBPW", context.Configuration["DB_PW"])
                            .ToString();
                        options.UseSqlServer(connectionString);
                    });
                })
                .ConfigureLogging((context, cfg) =>
                {
                    cfg.ClearProviders();
                    cfg.AddConfiguration(context.Configuration.GetSection("Logging"));
                    cfg.AddConsole();
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CaloriesLibraryContext>();

                try
                {
                    Console.WriteLine("Waiting 10s for database initialization...");
                    Thread.Sleep(10 * 1000);
                    Console.WriteLine("Applying Migrations...");
                    dbContext.Database.Migrate();
                    Console.WriteLine("Migrations applied successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error applying migrations: {ex.Message}");
                }
            }
        }
    }
}
