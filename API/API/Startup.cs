using API.Identity;
using API.Service;
using API.Validators;
using AutoMapper;
using Data.DbContexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Web
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
            services.AddIdentity<User, Role>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<CaloriesLibraryContext>();

            services.AddControllers();

            services.AddDbContext<CaloriesLibraryContext>(options =>
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

            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IRepository<Meal>, MealRepository>();
            services.AddTransient<IRepository<MealLog>, MealLogRepository>();
            services.AddTransient<IRepository<UserNutrition>, UserNutritionRepository>();
            services.AddTransient<IRepository<UserWeight>, UserWeightRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IMealService, MealService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserManager, UserManager>();

            services.AddTransient<IProductValidator, ProductValidator>();
            services.AddTransient<IMealValidator, MealValidator>();

            services.AddHttpContextAccessor();

            services.AddAuthentication()
            .AddCookie()
            .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _configuration["Token:Issuer"],
                        ValidAudience = _configuration["Token:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TOKEN_KEY"]))
                    };
                    cfg.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["X-Access-Token"];
                            return Task.CompletedTask;
                        },
                    };
                }
            );
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

            //app.UseHttpsRedirection();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

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
