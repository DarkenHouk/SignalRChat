namespace SignalRChat.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<Message> Messages { get; set; } = new List<Message>();
        public IEnumerable<UserChatRoom> UserChatRooms { get; set; } = new List<UserChatRoom>();
    }
}
