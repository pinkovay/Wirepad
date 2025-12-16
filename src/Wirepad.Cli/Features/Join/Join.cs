using System;
using Microsoft.AspNetCore.SignalR.Client;
using Wirepad.Cli.Common.Contracts;

namespace Wirepad.Cli.Features.Join;

public class Join(IHubConnectionProvider connectionProvider, IOutputFormatter outputFormatter) : IJoin
{
    private readonly IHubConnectionProvider _connectionProvider = connectionProvider;
    private readonly IOutputFormatter _outputFormatter = outputFormatter;

    public async Task JoinRoomAsync(string roomName)
    {
        _connectionProvider.OnReceiveContent(HandleContentReceived);

        // Conectando ou buscando o hub do SignalR.
        var connectionContract = await _connectionProvider.GetConnectedHubAsync();
        if(connectionContract == null) return;

        try
        {
            await connectionContract.InvokeAsync("JoinRoom", roomName);

            await Task.Delay(1000);
        } 
        catch (Exception ex)
        {
            _outputFormatter.WriteError($"[ ERROR ] Failed to access room '{roomName}': {ex.Message}");
        }
        finally
        {
            _connectionProvider.OnReceiveContentRemove(HandleContentReceived);
            await connectionContract.StopAsync();
        }
    }

    private void HandleContentReceived(string content)
    {
        _outputFormatter.DisplayContent(content);
    }
}

