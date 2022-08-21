using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Services
{
    public class UserService : IUserService
    {

        readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetByUserName(string userName)
        {
            var user = await _userRepository.GetByUserName(userName);
            if(user == null)
            {
                throw new Exception();
            }
            return user;
        }

        public async Task<User> GetUserById(int userId)
        {
            var user = await _userRepository.GetById(userId);

            if(user==null)
            {
                throw new Exception();
            }
            return user;
        }

        public async Task<User?> ConfirmPassword(string userName, string password)
        {
            var expectedUser = await _userRepository.GetByUserName(userName);
            if (expectedUser == null || expectedUser.Password != password)
            {
                throw new Exception();
            }

            return expectedUser;
        }


        public async Task<User?> CreateUserClient(User user)
        {
            var existUser = await _userRepository.GetByUserName(user.UserName);
            if (existUser == null)
            {
                existUser = await _userRepository.GetByEmail(user.Email);
                if (existUser == null)
                {
                    await _userRepository.AddUser(user);
                    await _userRepository.SaveChanges();

                    return user;
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<IEnumerable<User>> GetListOfUsers(User user)
        {
            var list = await _userRepository.GetList(user);
            return list;
        }
    }
}
