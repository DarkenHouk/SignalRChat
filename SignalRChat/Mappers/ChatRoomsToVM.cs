using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Mappers
{
    public class ChatRoomsToVM: IChatRoomsToVM
    {
        readonly IUserService _userService;

        public ChatRoomsToVM(
            IUserService userService)
        {
            _userService = userService;
        }
        public IList<Task<ChatRoomVM>> Map(IEnumerable<ChatRoom> chatRooms)
        {

            var maps = chatRooms.Select(PrivateMap).ToList();
            return maps;
        }

        private async Task<ChatRoomVM> PrivateMap(ChatRoom chatRoom)
        {
            ChatRoomVM map = new ChatRoomVM()
            {
                Id = chatRoom.Id
            };

            var userArray = chatRoom.UserChatRooms.ToArray();

            for (int i = 0; i< userArray.Length; i++)
            {
                var user = await _userService.GetUserById(userArray[0].UserId);
                map.Name += user.UserName + "\t";
            }
            if (userArray.Length > 2)
            {
                map.Name += $"and {userArray.Length - 2} other users";
            }
     
            return map;
        }
    }
}
