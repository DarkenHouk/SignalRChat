using Microsoft.EntityFrameworkCore;
using SignalRChat.DB;
using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ChatContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var message = await DbSet.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return (message is not null);
        }
    }
}
