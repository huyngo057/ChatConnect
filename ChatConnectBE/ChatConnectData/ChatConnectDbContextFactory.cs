using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ChatConnectData;

public class ChatConnectDbContextFactory : IDesignTimeDbContextFactory<ChatConnectContext>
{
	public ChatConnectContext CreateDbContext(string[] args)
	{
		var basePath = GetBasePath();

		var apiProjectPath =
			Path.Combine(basePath, "ChatConnectAPI");
		var appSettingsPath = Path.Combine(apiProjectPath, "appsettings.json");

		var configuration = new ConfigurationBuilder()
		                    .SetBasePath(apiProjectPath)
		                    .AddJsonFile(appSettingsPath)
		                    .Build();
		var builder = new DbContextOptionsBuilder<ChatConnectContext>();
		var connectionString = configuration.GetConnectionString("DefaultConnection");
		builder.UseMySql(connectionString ?? throw new InvalidOperationException(),
			ServerVersion.AutoDetect(connectionString));

		return new ChatConnectContext(builder.Options);
	}

	private string GetBasePath()
	{
		var basePath = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;

		if (basePath is null)
		{
			throw new InvalidOperationException("Could not find the base path.");
		}

		while (!File.Exists(Path.Combine(basePath, "ChatConnectBE.sln")))
		{
			basePath = Directory.GetParent(basePath)?.FullName;

			if (basePath is null)
			{
				throw new InvalidOperationException("Could not find the solution file 'ChatConnectBE.sln'.");
			}
		}

		return basePath;
	}
}