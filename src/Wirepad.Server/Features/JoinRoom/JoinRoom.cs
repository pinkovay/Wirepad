using System;
using Microsoft.AspNetCore.SignalR;
using Wirepad.Server.Domain.Room;

namespace Wirepad.Server.Features.JoinRoom;

public class JoinRoom(IRoomManager roomManager, IHubContext<PadHub> hubContext) : IJoinRoom
{
    private readonly IRoomManager _roomManager = roomManager;
    private readonly IHubContext<PadHub> _hubContext = hubContext;

    public async Task<string> HandleJoinRoomAsync(string roomName, string connectionId)
    {
        if (string.IsNullOrWhiteSpace(roomName))
                return string.Empty;

        string normalizedName = roomName.ToLowerInvariant();

        var room = _roomManager.GetOrCreateRoom(normalizedName);

        await _hubContext.Groups.AddToGroupAsync(connectionId, normalizedName);

        return room.Content;
    }
}

