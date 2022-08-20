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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(User user)
        {
            try
            {
                var res = await _userRepository.CreateUserClient(user);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return View();
        }

    }
}
