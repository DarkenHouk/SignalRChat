using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;

namespace SignalRChat.DB
{
    public static class DataSeeder
    {
        public static void SeedAdmin(this ModelBuilder modelBuilder)
        {
            const string userName = "Admin";
            const string email = "admin.mail@gmail.com";

            var admin = new User()
            {
                UserName = userName,
                Id = 1,
                Email = email,
                Password = "12345"
            };
            // seed admin
            modelBuilder.Entity<User>().HasData(admin);
        }
    }
}
