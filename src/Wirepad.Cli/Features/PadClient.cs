using System;
using Wirepad.Cli.Features.Join;
using Wirepad.Cli.Features.Send;
using Wirepad.Cli.Features.Watch;

namespace Wirepad.Cli.Features;

public class PadClient(IJoin join, ISend send, IWatch watch)
{
    public Task JoinRoomAsync(string roomName) => join.JoinRoomAsync(roomName); 
    public Task WatchRoomAsync(string roomName) => watch.WatchRoomAsync(roomName);
    public Task SendContentAsync(string roomName, string message) => send.SendContentAsync(roomName, message);
}

