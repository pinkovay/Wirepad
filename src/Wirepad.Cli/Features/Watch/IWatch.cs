using System;

namespace Wirepad.Cli.Features.Watch;

public interface IWatch
{
    Task WatchRoomAsync(string roomName);
}
