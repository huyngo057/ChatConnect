using ChatConnectBE.Services;
using ChatConnectData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRoomService, RoomService>();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();
app.UseCors();
// app.UseHttpsRedirection();
app.Run();