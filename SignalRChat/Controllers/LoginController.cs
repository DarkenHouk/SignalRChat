using Microsoft.AspNetCore.Mvc;
using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    public class LoginController : Controller
    {
        readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
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
                var result = await _userService.ConfirmPassword(user.UserName, user.Password);
                if (result != null)
                {
                    var userForChat = await _userService.GetByUserName(user.UserName);
                    return RedirectToAction("ChatList", "ChatList", userForChat);
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
