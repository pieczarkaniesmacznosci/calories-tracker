using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using App.Tracly.Models;
using Microsoft.AspNetCore.Identity;

namespace App.Tracly.Data
{
    public class AuthenticationDbContext : IdentityDbContext<User, Role, int>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedUserTable(builder);
            base.OnModelCreating(builder);
        }

        private void SeedUserTable(ModelBuilder modelBuilder)
        {
            var user = new User
            {
                Id = 1,
                FirstName = "First",
                LastName = "Last",
                Email = "email@domain.com",
                UserName = "email@domain.com",
                NormalizedUserName = "EMAIL@DOMAIN.COM",
                EmailConfirmed = true
            };
            PasswordHasher<User> ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, "support");

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN", Description = "Administration role" }
            );

            modelBuilder.Entity<User>().HasData(user);
        }
    }
}