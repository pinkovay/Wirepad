using System;
using System.CommandLine;
using Wirepad.Cli.Cli.Options;
using Wirepad.Cli.Features;

namespace Wirepad.Cli.Cli;

public class JoinCommand : Command
{
    private readonly PadClient _padClient;

    public JoinCommand(PadClient padClient)
        : base("join", "Joins the room and displays the current content.")
    {
        _padClient = padClient;

        var roomOption = RoomOption.Create();
        AddOption(roomOption);

        this.SetHandler(ExecuteAsync, roomOption);
    }

    private Task ExecuteAsync(string roomName)
    {
        return _padClient.JoinRoomAsync(roomName);
    }
}
