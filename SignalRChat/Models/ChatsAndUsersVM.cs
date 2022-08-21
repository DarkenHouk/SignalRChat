namespace SignalRChat.Models
{
    public class ChatsAndUsersVM
    {
        public IEnumerable<Task<ChatRoomVM>> ChatRooms{ get; set; }
        public IEnumerable<UserVM> Users { get; set; }
    }
}
