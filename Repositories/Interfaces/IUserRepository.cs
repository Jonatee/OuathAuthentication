using OuathAuthentication.Entities;

namespace OuathAuthentication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUser(string email);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> CheckIfUserExist(string email);
        Task<User> AddAsync(User user);
    }
}
