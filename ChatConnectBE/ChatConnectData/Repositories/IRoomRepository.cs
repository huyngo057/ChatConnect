using ChatConnectData.Models;

namespace ChatConnectData.Repositories;

public interface IRoomRepository
{
	Task<IEnumerable<Room>> GetAllRoomsAsync();
	Task<Room?> GetRoomByIdAsync(int id);
}