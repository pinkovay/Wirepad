
namespace Wirepad.Server.Domain.Room
{
    public interface IRoomManager
    {
        Room GetOrCreateRoom(string roomName);
        Room? UpdateRoomContent(string roomName, string newContent);
        string GetRoomContent(string roomName);
        bool RoomExists(string roomName);
    }
}