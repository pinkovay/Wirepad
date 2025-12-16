using System;
using System.CommandLine;

namespace Wirepad.Cli.Cli.Options;

public static class RoomOption
{
    public static Option<string> Create()
    {
        return new Option<string>(
            aliases: ["-r", "--room"],
            description: "The unique name of the room.")
        {
            IsRequired = true
        };
    }
}
