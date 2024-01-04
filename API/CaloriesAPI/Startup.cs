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
using Microsoft.OpenApi.Models;
using Repositories;
using System.Collections.Generic;
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
            services.AddCustomJwtAuthentication();

            services.AddDbContext<TraclyDbContext>(options =>
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

            services.AddTransient<IProductValidator, ProductValidator>();
            services.AddTransient<IMealValidator, MealValidator>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddHttpContextAccessor();

            services.AddAuthorization();

            services.AddSwaggerGen(
                c =>
                {
                    c.AddSecurityDefinition("Bearer",
                        new OpenApiSecurityScheme
                        {
                            Description = @"JWT Authorization header using the Bearer scheme.
                                Enter 'Bearer' [space] and then your token in the text input below.
                                Example: 'Bearer 12345abcdef'",
                            In = ParameterLocation.Header,
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer"
                        });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                });
                });
        }

        public void Configure(IApplicationBuilder app,
        IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
