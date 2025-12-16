using System;
using Microsoft.AspNetCore.SignalR;
using Wirepad.Server.Domain.Room;

namespace Wirepad.Server.Features.SendContent;

public class SendContent(IRoomManager roomManager, IHubContext<PadHub> hubContext) : ISendContent
{
    private readonly IRoomManager _roomManager = roomManager;
    private readonly IHubContext<PadHub> _hubContext = hubContext;

    public async Task<string> HandleSendContentAsync(string roomName, string newContent)
    {
        if (string.IsNullOrWhiteSpace(roomName) || string.IsNullOrWhiteSpace(newContent))
        {
            return string.Empty;
        }
        
        string normalizedName = roomName.ToLowerInvariant();

        var room = _roomManager.GetOrCreateRoom(normalizedName);
        _roomManager.UpdateRoomContent(room.Name, newContent);

        await _hubContext.Clients.Group(normalizedName)
                .SendAsync("ReceiveContent", room.Content);

        return normalizedName;
    }
}
