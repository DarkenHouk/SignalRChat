using Microsoft.AspNetCore.Mvc;
using SignalRChat.Interfaces;
using SignalRChat.Models;
using SignalRChat.Extenstions;

namespace SignalRChat.Controllers
{
    public class ChatListController : Controller
    {
        readonly IChatRoomService _chatRoomService;
        readonly IChatRoomsToVM _chatRoomsToVM;

        readonly IUserService _userService;
        readonly IUsersToVM _usersToVM;

        readonly IMessageService _messageService;

        public ChatListController(
            IChatRoomService chatRoomService,
            IChatRoomsToVM chatRoomsToVM,
            IUserService userService,
            IUsersToVM usersToVM,
            IMessageService messageService)
        {
            _chatRoomService = chatRoomService;
            _chatRoomsToVM = chatRoomsToVM;
            _userService = userService;
            _usersToVM = usersToVM;
            _messageService = messageService;
        }

        public async Task<IActionResult> ChatList(User client)
        {
            try
            {
                var chatRooms = await _chatRoomService.GetChatRoomsForUserAsync(client.Id);
                var chatRoomsVM = _chatRoomsToVM.Map(chatRooms);

                var users = await _userService.GetListOfUsers(client);
                var usersVM = _usersToVM.Map(users);

                var model = new ChatsAndUsersVM()
                {
                    Client = client,
                    ChatRooms = chatRoomsVM,
                    Users = usersVM
                };

                return View(model);

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return View();
        }

        public async Task<IActionResult> PrivateChat(string clientName, string userName)
        {
            var client = await _userService.GetByUserName(clientName);
            var user = await _userService.GetByUserName(userName);
            var chatRoom = await _chatRoomService.EnsurePrivateRoomCreatedAsync(user.Id, client.Id);
            //var messages = await _messageService.GetMessagesInChatRoomAsync(chatRoom.Id, 0,20);
            //chatRoom.Messages = messages;

            //var members = await _chatRoomService.GetUsersForChatRoom(chatRoom.Id);

            //var membersVM = _usersToVM.Map(members);

            //var clientAndChat = new UserChatVM()
            //{
            //    Client = client,
            //    Chat = chatRoom,
            //    Members = membersVM
            //};
            //return View("Messages", clientAndChat);
            return await Messages(client.Id, chatRoom.Id);
        }
        private async Task<IActionResult> ChatList(User client, ChatRoom chat)
        {
            return View();
        }

        public async Task<IActionResult> SendMessage(int chatRoomId, int senderId, string text)
        {
            var message = new Message()
            {
                ChatRoomId = chatRoomId,
                SenderId = senderId,
                Text = text,
                SentAt = DateTime.Now
            };

            await _messageService.CreateAsync(message);
            return await Messages(senderId, chatRoomId);
        }

        public async Task<IActionResult> SendText(UserChatVM userChatVM)
        {
            return await SendMessage(userChatVM.Chat.Id, userChatVM.Client.Id, userChatVM.Text);
        }

        private async Task<IActionResult> Messages(int clientId, int chatId)
        {
            var client = await _userService.GetUserById(clientId);
            var chatRoom = await _chatRoomService.GetByIdAsync(chatId);
            var messages = await _messageService.GetMessagesInChatRoomAsync(chatRoom.Id, 0, 20);
            var reverse = messages.Reverse();
            chatRoom.Messages = reverse;
            var members = await _chatRoomService.GetUsersForChatRoom(chatId);
            var membersVM = _usersToVM.Map(members);

            var clientAndChat = new UserChatVM()
            {
                Client = client,
                Chat = chatRoom,
                Members = membersVM
            };
            return View("Messages",clientAndChat);
        }
    }
}
