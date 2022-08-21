using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        Task<User?> GetByUserName(string userName);
        Task AddUser(User user);
        Task SaveChanges();
        Task<IEnumerable<User>> GetList(User user);
    }
}
