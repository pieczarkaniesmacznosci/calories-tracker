using API.Extensions;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace API.Identity
{
    public class UserManager : IUserManager
    {
        private IHttpContextAccessor _httpContextAccessor;
        private UserManager<User> _userManager;
        public int CurrentUserId => GetCurrentUserId().Result;
        public bool IsCurrentUserAdmin => IsCurrentUserAdminRole().Result;

        public UserManager(
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        private async Task<int> GetCurrentUserId()
        {
            var loggedInUserName = _httpContextAccessor.HttpContext.User.GetLoggedInUserNameIdentifier();
            var currentUserId = await _userManager.FindByNameAsync(loggedInUserName);

            return currentUserId.Id;
        }

        private async Task<bool> IsCurrentUserAdminRole()
        {
            var loggedInUserName = _httpContextAccessor.HttpContext.User.GetLoggedInUserNameIdentifier();
            var user = _userManager.FindByNameAsync(loggedInUserName).Result;
            var loggedInUserRole = await _userManager.IsInRoleAsync(user, "Admin");

            return loggedInUserRole;
        }
    }

    public interface IUserManager
    {
        int CurrentUserId { get; }
        bool IsCurrentUserAdmin { get; }
    }
}
