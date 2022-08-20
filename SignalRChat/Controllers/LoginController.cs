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
        public IActionResult Login()
        {
            User user = new User();
            return View();
        }

        public async Task<IActionResult> Login(User user)
        {
            var result = await _userRepository.ConfirmPassword(user.UserName, user.Password);
            if(result==null)
            {
                Login();
            }
            return View();
        }
    }
}
