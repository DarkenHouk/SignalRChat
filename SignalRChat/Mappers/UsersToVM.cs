using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Mappers
{
    public class UsersToVM:IUsersToVM
    {

        public IEnumerable<UserVM> Map(IEnumerable<User> users)
        {
            var maps = users.Select(PrivateMap).ToList();
            return maps;
        }

        private UserVM PrivateMap(User user)
        {
            UserVM map = new UserVM()
            {
                UserName = user.UserName
            };
            return map;
        }

    }
}
