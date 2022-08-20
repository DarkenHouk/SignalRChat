using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IChatRoomsToVM
    {
        IEnumerable<ChatRoomVM> Map(IEnumerable<ChatRoom> chatRooms);
    }
}
