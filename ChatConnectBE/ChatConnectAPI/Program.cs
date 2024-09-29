using System.Text;
using ChatConnectBE.Services;
using ChatConnectData;
using ChatConnectData.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IRoomRepository, RoomRepository>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<ChatConnectDbContext>()
       .AddDefaultTokenProviders();

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.Events = new JwtBearerEvents
	{
		OnMessageReceived = context =>
		{
			var token = context.HttpContext.Request.Cookies["jwt"];
			if (!string.IsNullOrEmpty(token))
			{
				context.Token = token;
			}

			return Task.CompletedTask;
		}
	};
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateLifetime = true,
		ValidateAudience = false,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
	};
});
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		policyBuilder =>
		{
			var allowedOrigins = builder.Configuration.GetSection("CORS:AllowedOrigins").Get<string[]>();
			if (allowedOrigins != null)
				policyBuilder.WithOrigins(allowedOrigins)
				             .AllowAnyHeader()
				             .AllowCredentials();
		});
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
	Console.Error.WriteLine("Error: Connection string 'DefaultConnection' is not set.");
	Environment.Exit(-1);
}

// Add data services to the container
Ioc.ConfigureServices(builder.Services, connectionString);
builder.Services.AddScoped<TokenService>();

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