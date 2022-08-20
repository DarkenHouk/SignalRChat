using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> CreateUserClient(User user);
        Task<User?> ConfirmPassword(string userName, string password);
    }
}
