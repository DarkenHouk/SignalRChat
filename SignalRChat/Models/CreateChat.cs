namespace SignalRChat.Models
{
    public class CreateChat
    {
        public UserVM Client { get; set; }
        public IEnumerable<UserVM> Users { get; set; } = new List<UserVM>();
        public IEnumerable<UserVM> SelectedUsers { get; set; } = new List<UserVM>();
        public UserVM AddOrRemoveUser { get; set; }
    }
}
