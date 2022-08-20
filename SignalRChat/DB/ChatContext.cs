using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;

namespace SignalRChat.DB
{
    public class ChatContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<UserChatRoom> UserChatRooms { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) 
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            builder.SeedAdmin();
        }
    }
}
