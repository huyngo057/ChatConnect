using ChatConnectData.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatConnectData;

public class ChatConnectContext : DbContext
{
	public ChatConnectContext(DbContextOptions<ChatConnectContext> options) : base(options)
	{
	}

	public DbSet<Room> Rooms { get; set; }
}