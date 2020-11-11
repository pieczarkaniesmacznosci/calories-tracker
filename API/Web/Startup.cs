using API.Web.DbContexts;
using API.Web.Repositories;
using API.Web.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using API.Web;
using API.Web.Service;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using API.Web.Validators;

namespace API
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

            services.AddControllers().AddMvcOptions(options =>
                options.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));

            var connectionString = _config["DefaultConnection"];
            services.AddDbContext<CaloriesLibraryContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<IRepository<Meal>, MealRepository>();
            services.AddTransient<IMealService, MealService>();

            services.AddTransient<IRepository<Meal>, MealRepository>();
            services.AddTransient<IMealService, MealService>();

            services.AddTransient<IRepository<UserNutrition>, UserNutritionRepository>();
            services.AddTransient<IRepository<UserWeight>, UserWeightRepository>();

            services.AddTransient<IUserService, UserService>();


            services.AddTransient<ProductValidator, ProductValidator>();
            services.AddTransient<MealValidator, MealValidator>();

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
                }
            );
            services.AddAuthorization();

            // Register the Swagger generator, defining 1 or more Swagger documents// Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
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
