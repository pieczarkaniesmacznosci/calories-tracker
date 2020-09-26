using Microsoft.AspNetCore.Identity;

namespace API.Web.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}