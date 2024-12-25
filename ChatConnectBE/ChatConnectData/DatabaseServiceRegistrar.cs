using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatConnectData;

public static class DatabaseServiceRegistrar
{
	public static void Register(IServiceCollection services, string connectionString)
	{
		services.AddDbContext<ChatConnectDbContext>(options =>
			options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
	}
}