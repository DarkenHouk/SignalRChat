using Microsoft.EntityFrameworkCore;
using SignalRChat.DB;
using SignalRChat.Interfaces;
using SignalRChat.Models;
using SignalRChat.Models.Enum;

namespace SignalRChat.Services
{
    public class ChatRoomService : IChatRoomService
    {
        readonly ChatContext _context;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IUserChatRoomRepository _userChatRoomRepository;
        private readonly IUserService _userService;
        public ChatRoomService(
            ChatContext chatContext,
            IChatRoomRepository chatRoomRepository,
            IUserChatRoomRepository userChatRoomRepository,
            IUserService userService)
        {
            _context = chatContext;
            _chatRoomRepository = chatRoomRepository;
            _userChatRoomRepository = userChatRoomRepository;
            _userService = userService;
        }

        public async Task CreateAsync(ChatRoom chatRoom)
        {
            await _chatRoomRepository.InsertAsync(chatRoom);
            await _chatRoomRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatRoom>> GetChatRoomsForUserAsync(int userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user is null)
            {
                throw new Exception();
            }

            var chatRooms = await _chatRoomRepository.QueryAsync(
                include: q => q.Include(cr => cr.UserChatRooms)
                                .ThenInclude(ur => ur.User),
                filter: cr => cr.UserChatRooms.Select(ur => ur.UserId).Contains(userId));

            return chatRooms;
        }

        public async Task<UserChatRoom?> GetUserChatRoomAsync(int userId, int chatRoomId)
        {
            return await _userChatRoomRepository.GetFirstOrDefaultAsync(
                filter: ur =>
                    ur.UserId == userId
                    && ur.ChatRoomId == chatRoomId);
        }

        public async Task<ChatRoom> EnsurePrivateRoomCreatedAsync(int memberId1, int memberId2)
        {
            var (member1, member2) = (await _userService.GetUserById(memberId1),
                                     await _userService.GetUserById(memberId2));
            if (member1 is null)
            {
                throw new Exception();
            }
            if (member2 is null)
            {
                throw new Exception();
            }

            var chatRoom = await _chatRoomRepository.GetFirstOrDefaultAsync(
                include: q => q.Include(cr => cr.UserChatRooms),
                filter: cr =>
                    cr.UserChatRooms.Select(ur => ur.UserId).Contains(memberId1)
                    && cr.UserChatRooms.Select(ur => ur.UserId).Contains(memberId2)
                    && cr.Type == ChatType.Private);

            if (chatRoom is null)
            {
                chatRoom = new ChatRoom()
                {
                    Type = ChatType.Private,
                    UserChatRooms = new[]
                    {
                    new UserChatRoom() { UserId = memberId1 },
                    new UserChatRoom() { UserId = memberId2 }
                }
                };
                await CreateAsync(chatRoom);
            }
            return chatRoom;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _chatRoomRepository.ExistsAsync(id);
        }

        public async Task<ChatRoom> GetByIdAsync(int chatRoomId)
        {
            var result = await _chatRoomRepository.GetById(chatRoomId);
            return result;
        }

        public async Task<IEnumerable<User>> GetUsersForChatRoom(int chatRoomId)
        {
            var chatRoom = await GetByIdAsync(chatRoomId);
            var users = new List<User>();

            foreach (var item in chatRoom.UserChatRooms)
            {
                var user = await _userService.GetUserById(item.UserId);
                users.Add(user);
            }
            return users;
        }
    }
}
