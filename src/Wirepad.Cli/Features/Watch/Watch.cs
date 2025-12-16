using System;
using Microsoft.AspNetCore.SignalR.Client;
using Wirepad.Cli.Common.Contracts;

namespace Wirepad.Cli.Features.Watch;

public class Watch(IHubConnectionProvider connectionProvider, IOutputFormatter outputFormatter) : IWatch
{
    private readonly IHubConnectionProvider _connectionProvider = connectionProvider;
    private readonly IOutputFormatter _outputFormatter = outputFormatter;

    private void HandleContentReceived(string content)
    {
         _outputFormatter.DisplayWatchUpdate(content);
        
    }
    
    public async Task WatchRoomAsync(string roomName)
    {
        _connectionProvider.OnReceiveContent(HandleContentReceived);

        var connectionContract = await _connectionProvider.GetConnectedHubAsync();
        if (connectionContract == null) return;
        
        try
        {
            await connectionContract.InvokeAsync("JoinRoom", roomName);
            
            await Task.Delay(Timeout.Infinite, new CancellationTokenSource().Token);
        }
        catch (TaskCanceledException)
        {
            _outputFormatter.WriteWarning("\nDisconnected. Watch session closed.");

        }
        catch (Exception ex)
        {
            _outputFormatter.WriteError($"\n[ ERROR ] An unexpected error occurred while watching: {ex.Message}");
        }
        finally
        {
            await connectionContract.StopAsync();
        }
    }
}
