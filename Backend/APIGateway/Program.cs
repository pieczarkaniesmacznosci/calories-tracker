using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
            if (builder.Configuration["RUN_PROFILE"] != "Local")
                env = "Docker";
            Console.WriteLine(env);
            builder.Configuration.AddJsonFile($"ocelot.json");
            builder.Configuration.AddJsonFile($"ocelot.{env}.json");

            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerForOcelot(builder.Configuration);
            builder.Services.AddOcelot(builder.Configuration);

            builder.Services.ConfigureDownstreamHostAndPortsPlaceholders(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerForOcelotUI(opt =>
                {
                    opt.PathToSwaggerGenerator = "/swagger/docs";
                });
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseOcelot().Wait();

            app.Run();
        }
    }
}
