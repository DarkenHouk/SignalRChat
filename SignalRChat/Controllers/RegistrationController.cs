using Microsoft.AspNetCore.Mvc;
using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    public class RegistrationController : Controller
    {
        readonly IUserRepository _userRepository;

        public RegistrationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Registration()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(User user)
        {
            var res = await _userRepository.CreateUserClient(user);

            return View();
        }

    }
}
