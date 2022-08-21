using Microsoft.EntityFrameworkCore;
using SignalRChat.DB;
using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ChatContext _context;

        public UserRepository(ChatContext context)
        {
            _context = context;
        }

        public async Task<User?> GetById(int id)
        {
            var user = await _context.Users
                .Where(x=> x.Id==id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetByUserName(string userName)
        {
            var user = await _context.Users
                .Where(x => x.UserName == userName)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return user;
        }
        public async Task<User?> GetByEmail(string email)
        {
            var user = await _context.Users
                .Where(x => x.Email == email)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetList(User user)
        {
            var list = await _context.Users
                .Where(u => u.UserName != user.UserName)
                .ToListAsync();
            return list;
        }


    }
}
