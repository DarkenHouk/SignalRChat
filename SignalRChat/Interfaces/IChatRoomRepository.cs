using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IChatRoomRepository : IRepository<ChatRoom>
    {
        Task<bool> ExistsAsync(int id);
    }
}
