using ChatConnectBE.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatConnectBE.Controllers;

[ApiController]
[Route("room")]
public class RoomController : Controller
{
	private readonly IRoomService _roomService;

	public RoomController(IRoomService roomService)
	{
		_roomService = roomService;
	}

	[HttpGet("all")]
	public async Task<IActionResult> GetAllRooms()
	{
		var rooms = await _roomService.GetAllRooms();
		return Ok(rooms);
	}

	[HttpPost]
	public IActionResult CreateRoom()
	{
		return Ok();
	}
}