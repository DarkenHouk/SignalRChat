using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByUserName(string userName);
        Task<User> GetUserById(int userId);
        Task<User?> CreateUserClient(User user);
        Task<User?> ConfirmPassword(string userName, string password);
        Task<IEnumerable<User>> GetListOfUsers(User user);
    }
}
