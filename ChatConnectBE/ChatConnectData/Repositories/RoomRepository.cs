using ChatConnectData.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatConnectData.Repositories;

public class RoomRepository : IRoomRepository
{
	private readonly ChatConnectContext _context;

	public RoomRepository(ChatConnectContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Room>> GetAllRoomsAsync()
	{
		return (await _context.Rooms.ToListAsync())!;
	}

	public async Task<Room?> GetRoomByIdAsync(int id)
	{
		return await _context.Rooms.FindAsync(id);
	}

	public async Task<Room> CreateRoomAsync(Room room)
	{
		await _context.Rooms.AddAsync(room);
		await _context.SaveChangesAsync();
		return room;
	}
}