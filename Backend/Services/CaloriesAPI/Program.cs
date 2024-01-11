using AutoMapper;
using CaloriesAPI.Validators;
using Common;
using Common.Auth;
using DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddCustomJwtAuthentication(builder.Configuration);

            builder.Services.AddDbContext<CaloriesDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionStringWithLoginCredentials("SqlServerLocal", "SqlServer");
                options.UseSqlServer(connectionString);
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(GenericAsyncRepository<>));
            builder.Services.AddScoped<DbContext, CaloriesDbContext>();
            builder.Services.AddTransient<IProductValidator, ProductValidator>();
            builder.Services.AddTransient<IProductValidator, ProductValidator>();
            builder.Services.AddTransient<IMealValidator, MealValidator>();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.AddSwaggerGenWithBearerToken();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CaloriesAPI v1");
            });

            app.UseRouting();
            ApplyMigration(app);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
            });
            app.Run();
        }
        static void ApplyMigration(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var _db = scope.ServiceProvider.GetRequiredService<CaloriesDbContext>();

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
