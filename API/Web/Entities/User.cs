using Microsoft.AspNetCore.Identity;

namespace API.Web.Entities
{
    public class User : IdentityUser
    {
        public int UserId {get;set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}