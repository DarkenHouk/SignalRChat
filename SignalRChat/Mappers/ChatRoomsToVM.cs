using SignalRChat.Models;

namespace SignalRChat.Mappers
{
    public class ChatRoomsToVM
    {
        public IEnumerable<ChatRoomVM> Map(IEnumerable<ChatRoom> chatRooms)
        {
            var maps = chatRooms.Select(PrivateMap).ToList();
            return maps;
        }

        private ChatRoomVM PrivateMap(ChatRoom chatRoom)
        {
            ChatRoomVM map = new ChatRoomVM()
            {
                Id = chatRoom.Id
            };
            return map;
        }
    }
}
