namespace SignalRChat.Models
{
    public class UserChatVM
    {
        public User Client { get; set; }
        public ChatRoom Chat { get; set; }
        public IEnumerable<UserVM> Members { get; set; }
        public string Text { get; set; }
    }
}
