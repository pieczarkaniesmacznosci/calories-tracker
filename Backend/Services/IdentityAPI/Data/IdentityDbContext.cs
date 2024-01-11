using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityAPI.Data
{
    public class IdentityDbContext : IdentityDbContext<User, Role, Guid>
    {
        private static readonly Guid _adminGuid = new("10000000-0000-0000-0000-000000000000");
        private static readonly Guid _userGuid = new("20000000-0000-0000-0000-000000000000");
        private static readonly Guid _adminRoleGuid = new("00000000-1000-0000-0000-000000000000");
        private static readonly Guid _userRoleGuid = new("00000000-2000-0000-0000-000000000000");
        private readonly IConfiguration _config;

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IConfiguration configuration) : base(options)
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
                Id = _adminGuid,
                FirstName = "Mike",
                LastName = "Smith",
                Email = _config["AdminName"],
                UserName = _config["AdminName"],
                NormalizedUserName = _config["AdminName"]!.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var regularUser = new User
            {
                Id = _userGuid,
                FirstName = "Dave",
                LastName = "Murray",
                Email = _config["RegularUserName"],
                UserName = _config["RegularUserName"],
                NormalizedUserName = _config["RegularUserName"]!.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            PasswordHasher<User> ph = new();
            user.PasswordHash = ph.HashPassword(user, _config["AdminPassword"]!);
            regularUser.PasswordHash = ph.HashPassword(regularUser, _config["RegularUserPassword"]!);

            modelBuilder.Entity<User>().HasData(user, regularUser);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = _adminRoleGuid, Name = "Admin", NormalizedName = "ADMIN", Description = "Administration role" },
                new Role { Id = _userRoleGuid, Name = "User", NormalizedName = "USER", Description = "User role" }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = Guid.NewGuid(), RoleId = _adminRoleGuid, UserId = _adminGuid },
                new UserRole { Id = Guid.NewGuid(), RoleId = _userRoleGuid, UserId = _userGuid }
            );
        }
    }
}
