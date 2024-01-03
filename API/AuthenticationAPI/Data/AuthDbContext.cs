using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Data
{
    public class AuthDbContext : IdentityDbContext<User, Role, int>
    {
        private readonly IConfiguration _config;

        public AuthDbContext(DbContextOptions<AuthDbContext> options, IConfiguration configuration) : base(options)
        {
            _config = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedUserTable(modelBuilder);
        }

        private void SeedUserTable(ModelBuilder modelBuilder)
        {
            var user = new User
            {
                Id = 1,
                FirstName = "Mike",
                LastName = "Smith",
                Email = _config["AdminName"],
                UserName = _config["AdminName"],
                NormalizedUserName = _config["AdminName"].ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var regularUser = new User
            {
                Id = 2,
                FirstName = "Dave",
                LastName = "Murray",
                Email = _config["RegularUserName"],
                UserName = _config["RegularUserName"],
                NormalizedUserName = _config["RegularUserName"].ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            PasswordHasher<User> ph = new();
            user.PasswordHash = ph.HashPassword(user, _config["AdminPassword"]);
            regularUser.PasswordHash = ph.HashPassword(regularUser, _config["RegularUserPassword"]);

            modelBuilder.Entity<User>().HasData(user, regularUser);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN", Description = "Administration role" },
                new Role { Id = 2, Name = "User", NormalizedName = "USER", Description = "User role" }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, RoleId = 1, UserId = 1 },
                new UserRole { Id = 2, RoleId = 2, UserId = 2 }
            );
        }
    }
}
