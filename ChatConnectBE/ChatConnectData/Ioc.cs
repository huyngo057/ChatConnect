using ChatConnectData.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatConnectData;

public static class Ioc
{
	public static void ConfigureServices(IServiceCollection services, string connectionString)
	{
		services.AddDbContext<ChatConnectContext>(options =>
			options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
		services.AddTransient<IRoomRepository, RoomRepository>();
	}
}