using System;
using Microsoft.AspNetCore.SignalR;
using Wirepad.Server.Features.JoinRoom;
using Wirepad.Server.Features.SendContent;

namespace Wirepad.Server.Features;

public class PadHub(IJoinRoom joinRoom, ISendContent sendContent) : Hub
{
    private readonly IJoinRoom _joinFeature = joinRoom;
    private readonly ISendContent _sendFeature = sendContent;

    public async Task JoinRoom(string roomName)
    {
        string connectionId = Context.ConnectionId;
 
        var roomContent = await _joinFeature.HandleJoinRoomAsync(roomName, connectionId);

        if (!string.IsNullOrEmpty(roomContent))
        {
            await Clients.Caller.SendAsync("ReceiveContent", roomContent);
        }
    }
    
    public async Task SendContent(string roomName, string newContent)
    {
        await _sendFeature.HandleSendContentAsync(roomName, newContent);
    }
}
