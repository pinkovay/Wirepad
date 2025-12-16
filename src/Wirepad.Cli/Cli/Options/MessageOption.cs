using System;
using System.CommandLine;

namespace Wirepad.Cli.Cli.Options;

public static class MessageOption
{
    public static Option<string> Create(bool isRequired = true)
    {
        return new Option<string>(
           aliases: ["-pd", "--padmessage"],
           description: "The message to be saved in the room.")
        {
            IsRequired = isRequired
        };
    }
}