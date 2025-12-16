using System;
using System.CommandLine;
using Wirepad.Cli.Cli.Options;
using Wirepad.Cli.Features;

namespace Wirepad.Cli.Cli;

public class WatchCommand : Command
{
    private readonly PadClient _padClient;

    public WatchCommand(PadClient padClient)
        : base("watch", "Watches the room in real time, receiving live updates.")
    {
        _padClient = padClient;

        var roomOption = RoomOption.Create();
        AddOption(roomOption);

        this.SetHandler(ExecuteAsync, roomOption);
    }

    private Task ExecuteAsync(string room)
    {
        return _padClient.WatchRoomAsync(room);
    }
}
