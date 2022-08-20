using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUserName(string userName);
        Task<User?> CreateUserClient(User user);
        Task<User?> ConfirmPassword(string userName, string password);
    }
}
