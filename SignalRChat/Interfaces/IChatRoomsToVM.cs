using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IChatRoomsToVM
    {
        IList<Task<ChatRoomVM>> Map(IEnumerable<ChatRoom> chatRooms);
    }
}
