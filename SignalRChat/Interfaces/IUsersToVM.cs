using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IUsersToVM
    {
        public IEnumerable<UserVM> Map(IEnumerable<User> users);
    }
}
