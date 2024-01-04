using JwtAuthenticationManager;
using UserService.Endpoints;
using UserService.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("SQL");
    return new SqlConnectionFactory(connectionString);
});
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

app.MapUserEndpoints(app.Services);

app.Run();
