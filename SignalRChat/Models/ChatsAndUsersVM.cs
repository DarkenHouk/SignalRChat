namespace SignalRChat.Models
{
    public class ChatsAndUsersVM
    {
        public User Client { get; set; }
        public IEnumerable<Task<ChatRoomVM>> ChatRooms{ get; set; }
        public IEnumerable<UserVM> Users { get; set; }
    }
}
