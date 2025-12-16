using System;

namespace Wirepad.Server.Features.JoinRoom;

public interface IJoinRoom
{
    Task<string> HandleJoinRoomAsync(string roomName, string connectionId);
}
