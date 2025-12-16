using System;
using Microsoft.AspNetCore.SignalR.Client;
using Wirepad.Cli.Common.Constants;
using Wirepad.Cli.Common.Contracts;

namespace Wirepad.Cli.SignalR
{
    public class HubConnectionProvider( IOutputFormatter outputFormatter, string serverUrl = DefaultServerUrl.Url) : IHubConnectionProvider
    {
        private readonly string _serverUrl = serverUrl;
        private readonly IOutputFormatter _outputFormatter = outputFormatter;
        private HubConnection? _hubConnection;
        
        // Ação para propagar o conteúdo recebido para os serviços de aplicação (Joiner/Watcher)
        private event Action<string>? ContentReceivedHandler; 

        // Implementação do contrato: Registra o manipulador de eventos de recebimento de conteúdo
        public void OnReceiveContent(Action<string> handler)
        {
            ContentReceivedHandler += handler;
        }

        // NOVO: Remove o manipulador de eventos (-=)
        public void OnReceiveContentRemove(Action<string> handler)
        {
            // O operador -= remove o ouvinte, prevenindo Memory Leaks.
            ContentReceivedHandler -= handler;
        }

        public async Task<HubConnection?> GetConnectedHubAsync()
        {
            if (_hubConnection == null)
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_serverUrl)
                    .WithAutomaticReconnect() 
                    .Build();

                // Registra o evento de recebimento do Hub e propaga para a Action registrada
                _hubConnection.On<string>("ReceiveContent", (content) =>
                {    
                    ContentReceivedHandler?.Invoke(content);
                });

                _hubConnection.Reconnecting += error =>
                {
                    _outputFormatter.WriteWarning("\n[ WARNING ] Connection lost. Attempting to reconnect...");
                    return Task.CompletedTask;
                };
            }

            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                try
                {
                    await _hubConnection.StartAsync();
                    return _hubConnection;
                }
                catch (Exception ex)
                {
                    _outputFormatter.WriteError($"[ ERROR ] Unable to connect to server at {_serverUrl}: {ex.Message}");
                    _outputFormatter.WriteWarning("Make sure Wirepad Server is running on port 5274.");

                    return null;
                }
            }

            return _hubConnection.State == HubConnectionState.Connected 
                ? _hubConnection
                : null;
        }
    }
}