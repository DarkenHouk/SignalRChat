using Microsoft.EntityFrameworkCore;
using SignalRChat.DB;
using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Repositories
{
    public class ChatRoomRepository : Repository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(ChatContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var chatRoom = await DbSet.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return (chatRoom is not null);
        }
    }
}
