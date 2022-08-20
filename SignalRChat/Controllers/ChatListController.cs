using Microsoft.AspNetCore.Mvc;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    public class ChatListController : Controller
    {
        public IActionResult ChatList(User user)
        {
            var chatlist = new ChatListVM();

            return View();
        }
    }
}
