using AutoMapper;
using CaloriesAPI.Validators;
using DbContexts;
using JwtAuthenticationManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories;
using System.Linq;
using System.Reflection;
using System.Text;

namespace API
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCustomJwtAuthentication(_configuration);

            services.AddDbContext<CaloriesDbContext>(options =>
            {
                string connectionStingName = "SqlServer";
                if (_configuration["TRACLY_PROFILE"] == "Local")
                    connectionStingName = "SqlServerLocal";

                var rawConnectionString = new StringBuilder(_configuration.GetConnectionString(connectionStingName));
                var connectionString = rawConnectionString
                    .Replace("ENVID", _configuration["DB_UID"])
                    .Replace("ENVDBPW", _configuration["DB_PW"])
                    .ToString();
                options.UseSqlServer(connectionString);
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped(typeof(IAsyncRepository<>), typeof(GenericAsyncRepository<>));
            services.AddScoped<DbContext, CaloriesDbContext>();
            services.AddTransient<IProductValidator, ProductValidator>();
            services.AddTransient<IProductValidator, ProductValidator>();
            services.AddTransient<IMealValidator, MealValidator>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddHttpContextAccessor();
            services.AddSwaggerGenWithBearerToken();
            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app,
        IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
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
                endpoints.MapControllers();
            });
        }

        static void ApplyMigration(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var _db = scope.ServiceProvider.GetRequiredService<CaloriesDbContext>();

            if (_db.Database.GetPendingMigrations().Any())
            {
                _db.Database.Migrate();
            }
        }
    }
}
