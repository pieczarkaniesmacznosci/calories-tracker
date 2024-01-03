using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
    }

    public class UserRole : IdentityUserRole<int>
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public int Id { get; set; }
    }
}
