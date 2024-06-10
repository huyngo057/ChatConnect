using ChatConnectData.Models;

namespace ChatConnectBE.Services;

public interface IRoomService
{
	public Task<IEnumerable<Room>> GetAllRooms();

	public Task<Room> CreateRoom(Room room);
}