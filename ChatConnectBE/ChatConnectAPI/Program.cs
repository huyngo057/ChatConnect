using ChatConnectBE.Extensions;
using ChatConnectData;

var builder = WebApplication.CreateBuilder(args);
var allowedOrigins = builder.Configuration.GetSection("CORS:AllowedOrigins").Get<string[]>();
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "";
var jwtKey = builder.Configuration["Jwt:Key"];
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtKey))
{
	throw new InvalidOperationException("Missing JWT configuration values.");
}

// Add services to the container.
builder.Services.ConfigureCoreServices();
builder.Services.ConfigureServices();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwtAuthentication(jwtIssuer, jwtAudience, jwtKey);

builder.Services.ConfigureCors(allowedOrigins);
builder.Services.AddAuthorization();

// Add data services to the container
if (string.IsNullOrEmpty(connectionString))
{
	Console.Error.WriteLine("Error: Connection string 'DefaultConnection' is not set.");
	Environment.Exit(-1);
}
DatabaseServiceRegistrar.Register(builder.Services, connectionString);


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();