using SignalRChat.Models;

namespace SignalRChat.Interfaces
{
    public interface IUsersToVM
    {
        UserVM Map(User user);
        IEnumerable<UserVM> Map(IEnumerable<User> users);
    }
}
