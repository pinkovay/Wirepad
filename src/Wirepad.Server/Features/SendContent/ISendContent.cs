using System;

namespace Wirepad.Server.Features.SendContent;

public interface ISendContent
{
    Task<string> HandleSendContentAsync(string roomName, string newContent);
}

