using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<UserNutrition> UserNutritions { get; set; }
        public IEnumerable<UserWeight> UserWeights { get; set; }
        public IEnumerable<MealLog> MealLogs { get; set; }
        public IEnumerable<Meal> Meals { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
    }
    public class UserRole : IdentityUserRole<int>
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public int Id {get;set;}
    }
}