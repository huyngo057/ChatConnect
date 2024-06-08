namespace ChatConnectBE.Responses;

public class GetAllRoomsDto
{
	public IList<GetRoomDto> RoomDtos { get; set; }
}