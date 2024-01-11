using Common;
using Common.Auth;
using Microsoft.EntityFrameworkCore;
using UserAPI.DbContexts;
using UserService.Endpoints;
using UserService.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionStringWithLoginCredentials("SqlServerLocal", "SqlServer");
    return new SqlConnectionFactory(connectionString);
});

builder.Services.AddDbContext<UserDbContext>((options =>
{
    var connectionString = builder.Configuration.GetConnectionStringWithLoginCredentials("SqlServerLocal", "SqlServer");
    options.UseSqlServer(connectionString);
}));

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCustomJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerGenWithBearerToken();
builder.Services.AddAuthorization();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoints();
ApplyMigration(app);
app.Run();

static void ApplyMigration(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var _db = scope.ServiceProvider.GetRequiredService<UserDbContext>();

    if (_db.Database.GetPendingMigrations().Any())
    {
        Console.WriteLine("Waiting 10s for database initialization...");
        Thread.Sleep(10 * 1000);
        Console.WriteLine("Applying Migrations...");
        _db.Database.Migrate();
        Console.WriteLine("Migrations applied successfully.");
    }
}
