using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<bool> ExistsAsync(int id);
    }
}
