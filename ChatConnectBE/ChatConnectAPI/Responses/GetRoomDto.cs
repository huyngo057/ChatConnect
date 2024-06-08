namespace ChatConnectBE.Responses;

public class GetRoomDto
{
	public string Id { get; set; }

	public string RoomName { get; set; }

	public string RoomType { get; set; }

	public string RoomDescription { get; set; }

	public string RoomOwnerName { get; set; }
}