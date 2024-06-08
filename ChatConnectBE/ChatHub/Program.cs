using ChatConnect.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		policyBuilder =>
		{
			var allowedOrigins = builder.Configuration.GetSection("CORS:AllowedOrigins").Get<string[]>();
			if (allowedOrigins != null)
				policyBuilder.WithOrigins(allowedOrigins)
					.AllowAnyHeader()
					.WithMethods("GET", "POST")
					.AllowCredentials();
		});
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.MapHub<ChatHub>("/chat-hub");
app.Run();