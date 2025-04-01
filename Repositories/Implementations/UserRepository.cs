using Microsoft.EntityFrameworkCore;
using OuathAuthentication.Entities;
using OuathAuthentication.Repositories.Interfaces;

namespace OuathAuthentication.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthContext _authContext;

        public UserRepository(AuthContext authContext) 
        {
            _authContext = authContext;
        }

        public async Task<User> AddAsync(User user)
        {
            await _authContext.Users.AddAsync(user);
            await _authContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> CheckIfUserExist(string email)
        {
           return await _authContext.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _authContext.Users.ToListAsync();
        }

        public async Task<User?> GetUser(string email)
        {
            return await _authContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
