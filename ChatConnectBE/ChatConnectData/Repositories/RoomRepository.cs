using ChatConnectData.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatConnectData.Repositories;

public class RoomRepository : IRoomRepository
{
	private readonly ChatConnectDbContext _dbContext;

	public RoomRepository(ChatConnectDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<IEnumerable<Room>> GetAllRoomsAsync()
	{
		return (await _dbContext.Rooms.ToListAsync())!;
	}

	public async Task<Room?> GetRoomByIdAsync(int id)
	{
		return await _dbContext.Rooms.FindAsync(id);
	}

	public async Task<Room> CreateRoomAsync(Room room)
	{
		await _dbContext.Rooms.AddAsync(room);
		await _dbContext.SaveChangesAsync();
		return room;
	}
}