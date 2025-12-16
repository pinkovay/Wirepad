using System;

namespace Wirepad.Server.Domain.Room;

public class Room
{
    public string Name {get; private set; }
    public string Content {get; private set; } = string.Empty;
    public DateTimeOffset LastUpdate {get; private set; }
    
    public Room(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Room name cannot be null or empty.", nameof(name));
        }

        Name = name.ToLowerInvariant();
        LastUpdate = DateTimeOffset.UtcNow;
    }

    public void UpdateContent(string newContent)
    {
        if(newContent is not null)
        {
            Content = newContent;
            LastUpdate = DateTimeOffset.UtcNow;
        }
    }

}
