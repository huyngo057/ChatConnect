using ChatConnectData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatConnectData;

public class ChatConnectDbContext : IdentityDbContext<IdentityUser>
{
	public ChatConnectDbContext(DbContextOptions<ChatConnectDbContext> options) : base(options)
	{
	}

	public DbSet<Room?> Rooms { get; set; }
}