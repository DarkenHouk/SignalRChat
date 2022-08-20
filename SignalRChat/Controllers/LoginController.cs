using Microsoft.AspNetCore.Mvc;
using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    public class LoginController : Controller
    {
        readonly IUserRepository _userRepository;
        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        private async Task<IActionResult> LoginStart()
        {
            //User user = new User();
            return await Task.Run(() => View());
        }

        private async Task<IActionResult> LoginAuth(User user)
        {
            try
            {
                var result = await _userRepository.ConfirmPassword(user.UserName, user.Password);
                if (result != null)
                {
                    return RedirectToAction("ChatList", "ChatList", new
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Messages = user.Messages,
                        UserChatRooms = user.UserChatRooms
                    });
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View();
        }

        public Task<IActionResult> Login(User user) => (user.UserName == null)&&(user.Password==null) ? LoginStart() : LoginAuth(user);
    }
}
