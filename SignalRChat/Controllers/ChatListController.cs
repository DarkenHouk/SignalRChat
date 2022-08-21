using Microsoft.AspNetCore.Mvc;
using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    public class ChatListController : Controller
    {
        readonly IChatRoomService _chatRoomService;
        readonly IChatRoomsToVM _chatRoomsToVM;

        readonly IUserService _userService;
        readonly IUsersToVM _usersToVM;

        public ChatListController(
            IChatRoomService chatRoomService,
            IChatRoomsToVM chatRoomsToVM,
            IUserService userService,
            IUsersToVM usersToVM)
        {
            _chatRoomService = chatRoomService;
            _chatRoomsToVM = chatRoomsToVM;
            _userService = userService;
            _usersToVM = usersToVM;
        }

        public async Task<IActionResult> ChatList(User user)
        {
            try
            {
                var chatRooms = await _chatRoomService.GetChatRoomsForUserAsync(user.Id);
                var chatRoomsVM = _chatRoomsToVM.Map(chatRooms);

                var users = await _userService.GetListOfUsers(user);
                var usersVM = _usersToVM.Map(users);

                var model = new ChatsAndUsersVM()
                {
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
    }
}
