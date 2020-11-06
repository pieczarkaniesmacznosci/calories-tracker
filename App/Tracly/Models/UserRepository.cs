using System.Collections.Generic;
using System.Threading.Tasks;
using API.Web.Entities;
using Microsoft.AspNetCore.Identity;

namespace App.Tracly.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _ph = new PasswordHasher<User>();
            var user = new User
            {
                Id = 1,
                FirstName = "First",
                LastName = "Last",
                Email = "email@domain.com",
                UserName = "email@domain.com",
                NormalizedUserName = "EMAIL@DOMAIN.COM",
            };
            user.PasswordHash = _ph.HashPassword(user, "support");

            _users = new List<User>
            {
                user
            };
        }

        private PasswordHasher<User> _ph;
        private List<User> _users;

        public async Task<User> GetByUsernameAndPassword(string username, string password)
        {

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!result.Succeeded)
            {
                return null;
            }

            return user;
        }
    }
}