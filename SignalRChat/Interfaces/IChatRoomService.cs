using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IChatRoomService
    {
        Task CreateAsync(ChatRoom chatRoom);
        Task<IEnumerable<ChatRoom>> GetChatRoomsForUserAsync(int userId);
        Task<ChatRoom> EnsurePrivateRoomCreatedAsync(int memberId1, int memberId2);
        Task<UserChatRoom?> GetUserChatRoomAsync(int userId, int chatRoomId);
        Task<bool> ExistsAsync(int id);
        Task<ChatRoom> GetByIdAsync(int chatRoomId);
        Task<IEnumerable<User>> GetUsersForChatRoom(int chatRoomId);
    }
}
