using Microsoft.EntityFrameworkCore;
using SignalRChat.DB;
using SignalRChat.Interfaces;
using SignalRChat.Models;

namespace SignalRChat.Services
{
    public class UserRepository: IUserRepository
    {
        private ChatContext _context;

        public UserRepository(ChatContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUserName(string userName)
        {
            var user = await _context.Users
                .Where(x => x.UserName == userName)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return user;
        }

        private async Task<User?> GetByEmail(string email)
        {
            var user = await _context.Users
                .Where(x => x.Email == email)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return user;
        }

        private async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        private async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User?> CreateUserClient(User user)
        {
            var existUser = await GetByUserName(user.UserName);
            if(existUser==null)
            {
                existUser = await GetByEmail(user.Email);
                if(existUser == null)
                {
                    await AddUser(user);
                    await SaveChanges();

                    return user;
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }
        }


        public async Task<User?> ConfirmPassword(string userName, string password)
        {
            var expectedUser = await GetByUserName(userName);
            if(expectedUser == null || expectedUser.Password != password)
            { 
                throw new Exception();
            }

            return expectedUser;
        }
    }
}
