using Microsoft.AspNetCore.Identity;

namespace App.Tracly.Models
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

}