namespace ChatConnectBE.Services;

public class RoomService : IRoomService
{
	public IList<string> GetAllRoom()
	{
		return new List<string> { "room1", "room2" };
	}
}