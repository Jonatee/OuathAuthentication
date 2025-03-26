using Microsoft.EntityFrameworkCore;
using OuathAuthentication.Entities;

namespace OuathAuthentication
{
    public class AuthContext : DbContext 
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
    }
}
