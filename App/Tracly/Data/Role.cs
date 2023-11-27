using Microsoft.AspNetCore.Identity;

namespace Tracly.Data
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}
