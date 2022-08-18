using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;

namespace SignalRChat.DB
{
    public class ChatContext: DbContext
    {
        DbSet<User> Users { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            builder.SeedAdmin();
        }
    }
}
