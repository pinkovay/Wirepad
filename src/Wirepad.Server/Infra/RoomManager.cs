using System;
using System.Collections.Concurrent;
using Wirepad.Server.Domain.Room;

namespace Wirepad.Server.Infra;

public class RoomManager : IRoomManager
{
    private readonly ConcurrentDictionary<string, Room> _rooms = new(); // Multiplos threads podem acessar e modificar os dados sem corromper dados

    public Room GetOrCreateRoom(string roomName)
    {
        return _rooms.GetOrAdd(roomName.ToLowerInvariant(), (name) => new Room(name));
    }

    public string GetRoomContent(string roomName)
    {
        var normalizedName = roomName.ToLowerInvariant();
        
        if (_rooms.TryGetValue(normalizedName, out var room))
        {
            return room.Content;
        }
        
        return string.Empty;
    }

    public bool RoomExists(string roomName)
    {
        return _rooms.ContainsKey(roomName.ToLowerInvariant());
    }

    public Room? UpdateRoomContent(string roomName, string newContent)
    {
        if(newContent is null)
            return null;
        
        var room = GetOrCreateRoom(roomName.ToLowerInvariant());

        lock (room)
        {
            room.UpdateContent(newContent);
        }

        return room;
    }

    
}
