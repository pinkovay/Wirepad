using Wirepad.Server.Domain.Room;
using Wirepad.Server.Features;
using Wirepad.Server.Features.JoinRoom;
using Wirepad.Server.Features.SendContent;
using Wirepad.Server.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRoomManager, RoomManager>();
builder.Services.AddTransient<IJoinRoom, JoinRoom>();
builder.Services.AddTransient<ISendContent, SendContent>();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .SetIsOriginAllowed((host) => true)
              .AllowCredentials();
    });
});

var app = builder.Build();

app.UseRouting();

app.UseCors(); 

// Mapeia os endpoints, incluindo o SignalR Hub.
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapHub<PadHub>("/padhub");
});

app.Run();

