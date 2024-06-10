using ChatConnectData.Models;

namespace ChatConnectData.Repositories;

public interface IRoomRepository
{
	Task<IEnumerable<Room>> GetAllRoomsAsync();

	Task<Room> CreateRoomAsync(Room room);

	Task<Room?> GetRoomByIdAsync(int id);
}