using ChatConnectData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatConnectData;

public class ChatConnectDbContext(DbContextOptions<ChatConnectDbContext> options)
	: IdentityDbContext<IdentityUser>(options)
{
	public DbSet<Room> Rooms { get; set; }
}