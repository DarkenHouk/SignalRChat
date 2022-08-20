using Microsoft.EntityFrameworkCore;
using SignalRChat.DB;
using SignalRChat.Interfaces;
using SignalRChat.Models;
using SignalRChat.Models.Enum;

namespace SignalRChat.Services
{
    public class ChatRoomService: IChatRoomService
    {
        readonly ChatContext _context;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IUserChatRoomRepository _userChatRoomRepository;
        private readonly UserRepository _userService;
        public ChatRoomService(
            ChatContext chatContext,
            IChatRoomRepository chatRoomRepository,
            IUserChatRoomRepository userChatRoomRepository,
            UserRepository userService)
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

        public async Task<IEnumerable<ChatRoom>> GetChatRoomsForUserAsync(string userName)
        {
            var user = await _userService.GetByUserName(userName);
            if (user is null)
            {
                throw new Exception();
            }

            var chatRooms = await _chatRoomRepository.QueryAsync(
                include: q => q.Include(cr => cr.UserChatRooms)
                                .ThenInclude(ur => ur.User),
                filter: cr => cr.UserChatRooms.Select(ur => ur.UserId).Contains(user.Id));

            return chatRooms;
        }

        public async Task<UserChatRoom?> GetUserChatRoomAsync(string userName, int chatRoomId)
        {
            var user = await _userService.GetByUserName(userName);

            return await _userChatRoomRepository.GetFirstOrDefaultAsync(
                filter: ur =>
                    ur.UserId == user.Id
                    && ur.ChatRoomId == chatRoomId);
        }

        public async Task<ChatRoom> EnsurePrivateRoomCreatedAsync(string memberName1, string memberName2)
        {
            var (member1, member2) = (await _userService.GetByUserName(memberName1),
                                     await _userService.GetByUserName(memberName2));
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
                    cr.UserChatRooms.Select(ur => ur.UserId).Contains(member1.Id)
                    && cr.UserChatRooms.Select(ur => ur.UserId).Contains(member2.Id)
                    && cr.Type == ChatType.Private);

            if (chatRoom is null)
            {
                chatRoom = new ChatRoom()
                {
                    Type = ChatType.Private,
                    UserChatRooms = new[]
                    {
                    new UserChatRoom() { UserId = member1.Id },
                    new UserChatRoom() { UserId = member2.Id }
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
    }
}
