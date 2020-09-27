using System;
using System.Threading.Tasks;
using API.Web.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Web.DbContexts
{
    public class CaloriesLibraryInitializer
    {

        public static async Task SeedUsersAsync(CaloriesLibraryContext context, UserManager<User> userManager)
        {
            context.Database.EnsureCreated();
            
            var user = await userManager.FindByEmailAsync("email@domain.com");

            if(user == null)
            {
                user = new User()
                {
                    FirstName = "First",
                    LastName = "Last",
                    Email = "email@domain.com",
                    UserName = "email@domain.com"
                };
                var result = await userManager.CreateAsync(user, "P@ssw0rd");
                
                if(!result.Succeeded)
                {
                    throw new InvalidOperationException("Could not create new user");
                }
            }
        }   
    }
}
