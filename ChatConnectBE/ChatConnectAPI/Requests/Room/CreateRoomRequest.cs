namespace ChatConnectBE.Requests;

public class CreateRoomRequest
{
	public required string Name { get; set; }
	public required string Type { get; set; } = "Public";
	public required string Description { get; set; } = string.Empty;
}