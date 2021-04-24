using Data.Web.DbContexts;
using Data.Repositories;
using Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using API.Service;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using API.Identity;
using API.Validators;
using System;

namespace API.Web
{
    public class Startup
    {
        private IConfiguration _config { get; }

        public Startup(IConfiguration config)
        {
            _config = config;
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
                var config = new StringBuilder(_config["ConnectionString:SqlServer"]);
                var conn = config
                    .Replace("ENVID", _config["DB_UID"])
                    .Replace("ENVDBPW", _config["DB_PW"])
                    .ToString();
                options.UseSqlServer(conn);
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
                        ValidIssuer = _config["Tokens:Issuer"],
                        ValidAudience = _config["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
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

            // Register the Swagger generator, defining 1 or more Swagger documents// Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(
                c => {
                    c.AddSecurityDefinition("Bearer", 
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme{
                            Description = @"JWT Authorization header using the Bearer scheme.
                                Enter 'Bearer' [space] and then your token in the text input below.
                                Example: 'Bearer 12345abcdef'",
                            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                            Name = "Authorization",
                            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
        IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;  // Set Swagger UI at apps root
            });

            //MVC middleware will handel request
            // app.UseMvc();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
