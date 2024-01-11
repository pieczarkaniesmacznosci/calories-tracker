using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class User : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class Role : IdentityRole<Guid>
    {
        public string? Description { get; set; }
    }

    public class UserRole : IdentityUserRole<Guid>
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public Guid Id { get; set; }
    }
}
