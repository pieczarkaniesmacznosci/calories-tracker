using AuthenticationAPI.Data;
using AuthenticationAPI.Identity;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text;

namespace AuthenticationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddIdentity<User, Role>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AuthDbContext>();

            builder.Services.AddDbContext<AuthDbContext>(options =>
            {
                string connectionStingName = "SqlServer";
                if (builder.Configuration["TRACLY_PROFILE"] == "Local")
                    connectionStingName = "SqlServerLocal";

                var rawConnectionString = new StringBuilder(builder.Configuration.GetConnectionString(connectionStingName));
                var connectionString = rawConnectionString
                    .Replace("ENVID", builder.Configuration["DB_UID"])
                    .Replace("ENVDBPW", builder.Configuration["DB_PW"])
                    .ToString();
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddTransient<IUserManager, UserManager>();
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
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
        }
    }
}
