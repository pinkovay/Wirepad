using System;

namespace Wirepad.Cli.Features.Send;

public interface ISend
{
    Task SendContentAsync(string roomName, string message);
}
