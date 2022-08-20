using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IMessageService
    {
        Task<Message?> GetByIdAsync(int id);
        Task<IEnumerable<Message>> GetMessagesInChatRoomAsync(int chatRoomId, int skip, int take);
        Task<IEnumerable<Message>> GetUnreadMessagesAsync(string userName);
        Task CreateAsync(Message message);
        Task ReadAsync(int readerId, int messageId);
    }
}
