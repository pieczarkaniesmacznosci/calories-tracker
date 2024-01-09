using JwtAuthenticationManager;
using Microsoft.EntityFrameworkCore;
using System.Text;
using UserAPI.DbContexts;
using UserService.Endpoints;
using UserService.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton(serviceProvider =>
{
    var configuration = builder.Configuration;
    string connectionStingName = "SqlServer";
    if (builder.Configuration["TRACLY_PROFILE"] == "Local")
        connectionStingName = "SqlServerLocal";

    var rawConnectionString = new StringBuilder(builder.Configuration.GetConnectionString(connectionStingName));
    var connectionString = rawConnectionString
        .Replace("ENVID", builder.Configuration["DB_UID"])
        .Replace("ENVDBPW", builder.Configuration["DB_PW"])
        .ToString();
    return new SqlConnectionFactory(connectionString);
});

builder.Services.AddDbContext<UserDbContext>(options =>
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
        _db.Database.Migrate();
    }
}
