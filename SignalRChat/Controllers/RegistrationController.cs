using Microsoft.AspNetCore.Mvc;
using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    public class RegistrationController : Controller
    {
        readonly IUserService _userService;

        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(User user)
        {
            try
            {
                var res = await _userService.CreateUserClient(user);

                return RedirectToAction("Login", "Login", user);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return View();
        }

    }
}
