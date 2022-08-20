using Microsoft.AspNetCore.Mvc;
using SignalRChat.Interfaces;
using SignalRChat.Mappers;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    public class ChatListController : Controller
    {
        readonly IChatRoomService _chatRoomService;
        readonly ChatRoomsToVM _chatRoomsToVM;

        public ChatListController(
            IChatRoomService chatRoomService,
            ChatRoomsToVM chatRoomsToVM)
        {
            _chatRoomService = chatRoomService;
            _chatRoomsToVM = chatRoomsToVM;
        }

        public async Task<IActionResult> ChatList(User user)
        {
            var chatRooms = await _chatRoomService.GetChatRoomsForUserAsync(user.UserName);
            var chatRoomsVM = _chatRoomsToVM.Map(chatRooms);
            
            var chatListVM = new ChatListVM()
            {
                chatRooms = chatRoomsVM
            };


            return View(chatListVM);
        }
    }
}
