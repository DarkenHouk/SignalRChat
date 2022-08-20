using Microsoft.EntityFrameworkCore;
using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserChatRoomRepository _userChatRoomRepository;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IUserRepository _userService;

        public MessageService(
            IMessageRepository messageRepository,
            IUserChatRoomRepository userChatRoomRepository,
            IChatRoomRepository chatRoomRepository,
            IUserRepository userService)
        {
            _messageRepository = messageRepository;
            _userChatRoomRepository = userChatRoomRepository;
            _chatRoomRepository = chatRoomRepository;
            _userService = userService;
        }

        public async Task<Message?> GetByIdAsync(int id)
        {
            var message = await _messageRepository.GetFirstOrDefaultAsync(
                filter: m => m.Id == id);

            return message;
        }

        public async Task<IEnumerable<Message>> GetMessagesInChatRoomAsync(
            int chatRoomId, int skip, int take)
        {
            if (!await _chatRoomRepository.ExistsAsync(chatRoomId))
            {
                throw new Exception();
            }

            var messagesToRead = await _messageRepository.QueryAsync(
                filter: m => m.ChatRoomId == chatRoomId,
                orderBy: q => q.OrderByDescending(m => m.SentAt),
                include: q => q.Include(m => m.Sender!),
                skip: skip,
                take: take,
                asNoTracking: true
            );

            return messagesToRead;
        }

        public async Task<IEnumerable<Message>> GetUnreadMessagesAsync(string userName)
        {
            var user = await _userService.GetByUserName(userName);
            if (user is null)
                throw new Exception();

            var userChatRooms = await _userChatRoomRepository.QueryAsync(
                include: q =>
                    q.Include(ur => ur.LastReadMessage!)
                        .Include(ur => ur.ChatRoom)
                        .ThenInclude(cr => cr.Messages),
                filter: ur => ur.UserId == user.Id
            );

            var unreadMessages = userChatRooms.SelectMany(ur => ur.ChatRoom.Messages.Where(m =>
            {
                var lastReadDateTime = ur.LastReadMessage?.SentAt ?? DateTime.MinValue;
                return (m.SentAt > lastReadDateTime && m.SenderId != user.Id);
            }));

            return unreadMessages;
        }

        public async Task CreateAsync(Message message)
        {
            if (!await _chatRoomRepository.ExistsAsync(message.ChatRoomId))
            {
                throw new Exception();
            }

            await _messageRepository.InsertAsync(message);
            await _messageRepository.SaveChangesAsync();

        }

        public async Task ReadAsync(int readerId, int messageId)
        {
            var message = await GetByIdAsync(messageId);

            if (message is null)
            {
                throw new Exception();
            }

            var userChatRoom = await _userChatRoomRepository.GetFirstOrDefaultAsync(
                filter: ur => ur.UserId == readerId && ur.ChatRoomId == message.ChatRoomId,
                include: q => q.Include(ur => ur.LastReadMessage)!);

            if (userChatRoom is null)
            {
                throw new Exception();
            }

            if (userChatRoom.LastReadMessage is null || userChatRoom.LastReadMessage.SentAt < message.SentAt)
            {
                userChatRoom.LastReadMessageId = message.Id;
                await _messageRepository.SaveChangesAsync();

            }
        }
    }
}
