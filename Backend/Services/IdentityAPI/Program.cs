using Common;
using Entities;
using IdentityAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IdentityAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentity<User, Role>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityDbContext>();

            builder.Services.AddDbContext<IdentityDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionStringWithLoginCredentials("SqlServerLocal", "SqlServer");
                options.UseSqlServer(connectionString);
            });

            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();
            ApplyMigration(app);
            app.Run();
        }

        static void ApplyMigration(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var _db = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

            if (_db.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Waiting 10s for database initialization...");
                Thread.Sleep(10 * 1000);
                Console.WriteLine("Applying Migrations...");
                _db.Database.Migrate();
                Console.WriteLine("Migrations applied successfully.");
            }
        }
    }
}
