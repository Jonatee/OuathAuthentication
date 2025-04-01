using Microsoft.EntityFrameworkCore;
using OuathAuthentication.Entities;

namespace OuathAuthentication
{
    public class AuthContext : DbContext 
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Admin> Admins => Set<Admin>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            var userId = Guid.NewGuid();
            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = userId,
                    Email = "AdminUser@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("AdminUser", salt),
                    LastLoginAt = DateTime.Now,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Role = "Admin",

                }

            );


        }
    }

}
