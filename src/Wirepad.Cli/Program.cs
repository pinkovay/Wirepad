using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using Wirepad.Cli.Cli;
using Wirepad.Cli.Common;
using Wirepad.Cli.Common.Contracts;
using Wirepad.Cli.Features;
using Wirepad.Cli.Features.Join;
using Wirepad.Cli.Features.Send;
using Wirepad.Cli.Features.Watch;
using Wirepad.Cli.SignalR;

namespace Wirepad.Cli
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            var rootCommandService = host.Services.GetRequiredService<CommandBuilder>();
            
            var rootCommand = rootCommandService.BuildRootCommand();

            return await rootCommand.InvokeAsync(args);
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // Registra o provedor de conexão (Singleton, pois gerencia o estado persistente da conexão)
                    services.AddSingleton<IHubConnectionProvider, HubConnectionProvider>();

                    services.AddSingleton<IOutputFormatter, ConsoleOutputFormatter>();

                    // Registra os implementadores das interfaces de Feature (Transient, pois são lógicas de execução)
                    services.AddTransient<ISend, Send>();
                    services.AddTransient<IJoin, Join>();
                    services.AddTransient<IWatch, Watch>();

                    // O PadClient orquestra as Features e o RootCommandService orquestra o PadClient.
                    services.AddTransient<PadClient>();
                    services.AddTransient<CommandBuilder>();
                });
    }
}