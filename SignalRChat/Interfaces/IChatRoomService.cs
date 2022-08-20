using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IChatRoomService
    {
        Task CreateAsync(ChatRoom chatRoom);
        Task<IEnumerable<ChatRoom>> GetChatRoomsForUserAsync(string userName);
        Task<ChatRoom> EnsurePrivateRoomCreatedAsync(string memberName1, string memberName2);
        Task<UserChatRoom?> GetUserChatRoomAsync(string userName, int chatRoomId);
        Task<bool> ExistsAsync(int id);
    }
}
