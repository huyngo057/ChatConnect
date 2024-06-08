using ChatConnect.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatConnect.Hubs;

public class ChatHub: Hub
{
	private static readonly Dictionary<string, List<string>> _rooms = new();

	public async Task JoinRoom(string roomName, string userName)
	{
		await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
		if (!_rooms.ContainsKey(roomName))
		{
			_rooms[roomName] = new List<string>();	
		}
		_rooms[roomName].Add(userName);
		var systemMessage = $"{userName} has joined {roomName}";
		await Clients.Group(roomName).SendAsync("ReceiveSystemMessage", systemMessage);
	}

	public async Task SendMessageToRoom(string roomName, string userName, string message)
	{
		if (_rooms.ContainsKey(roomName) && _rooms[roomName].Contains(userName))
		{
			await Clients.Group(roomName).SendAsync("ReceiveMessage", userName, message);
		}
		else
		{
			Console.WriteLine($"User {userName} is not in the room {roomName}.");
		}
	}

	public async Task LeaveRoom(string roomName, string userName)
	{
		if (_rooms.ContainsKey(roomName) && _rooms[roomName].Contains(userName))
		{
			_rooms[roomName].Remove(userName);

			await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
			
			await Clients.Group(roomName).SendAsync("ReceiveSystemMessage", $"{userName} has left the room.");
		}
	}
}