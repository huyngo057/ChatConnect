using ChatConnectBE.Services;
using ChatConnectData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRoomService, RoomService>();
var app = builder.Build();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
	Console.Error.WriteLine("Error: Connection string 'DefaultConnection' is not set.");
	Environment.Exit(-1);
}

// Add data services to the container
Ioc.ConfigureServices(builder.Services, connectionString);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();
// app.UseHttpsRedirection();
app.Run();