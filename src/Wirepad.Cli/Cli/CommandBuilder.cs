using System;
using System.CommandLine;
using Wirepad.Cli.Features;

namespace Wirepad.Cli.Cli
{
    public class CommandBuilder(PadClient padClient)
    {
        private readonly PadClient _padClient = padClient;

        public RootCommand BuildRootCommand()
        {
            // 1. Comando JOIN (pad join -r teste)
            var joinCommand = new JoinCommand(_padClient);

            // 2. Comando WATCH (pad watch -r teste)
            var watchCommand = new WatchCommand(_padClient);

            // 3. Comando SEND (pad send -r teste -pd salve)
            var sendCommand = new SendCommand(_padClient);
            
            // --- Comando Raiz (Root Command) ---
            var rootCommand = new RootCommand("CLI tool for text exchange using SignalR.")
            {
                joinCommand,
                watchCommand,
                sendCommand
            };

            rootCommand.Name = "wirepad";

            return rootCommand;
        }
    }
}
