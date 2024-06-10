using ChatConnectData.Models;
using ChatConnectData.Repositories;

namespace ChatConnectBE.Services;

public class RoomService : IRoomService
{
	private readonly IRoomRepository _roomRepository;

	public RoomService(IRoomRepository roomRepository)
	{
		_roomRepository = roomRepository;
	}

	public async Task<IEnumerable<Room>> GetAllRooms()
	{
		return await _roomRepository.GetAllRoomsAsync();
	}
}