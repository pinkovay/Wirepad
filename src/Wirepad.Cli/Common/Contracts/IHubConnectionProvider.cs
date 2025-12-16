using System;
using Microsoft.AspNetCore.SignalR.Client;

namespace Wirepad.Cli.Common.Contracts;

public interface IHubConnectionProvider
{
    Task<HubConnection?> GetConnectedHubAsync();
    void OnReceiveContent(Action<string> handler);
    void OnReceiveContentRemove(Action<string> handler);
}
