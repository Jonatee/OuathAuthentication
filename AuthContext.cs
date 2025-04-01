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


            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = new Guid("e58b5d4c-8f57-4c0b-a2b5-6efc3a7a620c"),
                    Email = "AdminUser@gmail.com",
                    Password = "$2a$11$hsXlBxzfPhPmlqraxM4hlOV.z6Miz2/AHnT5.i2MX3XH/fqxVTOAm",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Role = "Admin",
                    LastLoginAt = new DateTime(2024, 3, 29, 12, 0, 0, DateTimeKind.Utc),
                    TimesOfLogin = 1
                }

            );


        }
    }

}
