using SignalRChat.DB;
using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Repositories
{
    public class UserChatRoomRepository : Repository<UserChatRoom>, IUserChatRoomRepository
    {
        public UserChatRoomRepository(ChatContext context) : base(context)
        {
        }
    }
}
