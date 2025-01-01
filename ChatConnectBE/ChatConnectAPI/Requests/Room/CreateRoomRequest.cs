using ChatConnectData.Enums;

namespace ChatConnectBE.Requests.Room;

public class CreateRoomRequest
{
	public required string Name { get; set; }
	public required string Type { get; set; } = RoomType.Public;
	public required string Description { get; set; } = string.Empty;
}