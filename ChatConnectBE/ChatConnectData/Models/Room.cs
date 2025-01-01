using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ChatConnectData.Enums;

namespace ChatConnectData.Models;

public class Room : BaseEntity<int>
{
	[Required]
	[MaxLength(300)]
	public required string Name { get; set; }
	
	[MaxLength(20)]
	public string Type { get; set; } = RoomType.Public;

	[MaxLength(500)]
	public string Description { get; set; } = string.Empty;
}