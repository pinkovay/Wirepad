using System;
using System.CommandLine;
using Wirepad.Cli.Cli.Options;
using Wirepad.Cli.Features;

namespace Wirepad.Cli.Cli
{
    public class SendCommand : Command
    {
        private readonly PadClient _padClient;

        public SendCommand(PadClient padClient) 
            : base("send", "Sends and overwrites the current room content.")
        {
            _padClient = padClient;

            var roomOption = RoomOption.Create();
            var messageOption = MessageOption.Create();
            
            AddOption(roomOption);
            AddOption(messageOption);

            this.SetHandler(ExecuteAsync, roomOption, messageOption);
        }

        private Task ExecuteAsync(string room, string message)
        {
            return _padClient.SendContentAsync(room, message);
        }
    }
}
