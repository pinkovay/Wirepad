using System;

namespace Wirepad.Cli.Features.Join;

public interface IJoin
{
    Task JoinRoomAsync(string roomName);
}
