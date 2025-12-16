using System;
using Microsoft.AspNetCore.SignalR.Client;
using Wirepad.Cli.Common.Contracts;

namespace Wirepad.Cli.Features.Send;

public class Send(IHubConnectionProvider connectionProvider, IOutputFormatter outputFormatter) : ISend
{
    private readonly IHubConnectionProvider _connectionProvider = connectionProvider;
    private readonly IOutputFormatter _outputFormatter = outputFormatter;

    public async Task SendContentAsync(string roomName, string message)
    {
        var connectionContract = await _connectionProvider.GetConnectedHubAsync();
        
        if (connectionContract == null) return;

        try
        {
            await connectionContract.InvokeAsync("SendContent", roomName, message);
            
            // Console.WriteLine($"\nConteúdo enviado com sucesso.");
            _outputFormatter.WriteSuccess("Conteúdo enviado com sucesso.");
        }
        catch (Exception ex)
        {
            _outputFormatter.WriteError($"[ ERRO ] Falha ao enviar conteúdo para '{roomName}': {ex.Message}");
        }
        finally
        {
            await connectionContract.StopAsync();
        }
    }
}
