using Microsoft.AspNetCore.Identity;

namespace Tracly.Data
{
    public class User : IdentityUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SecurityStamp { get; set; }
    }
}
