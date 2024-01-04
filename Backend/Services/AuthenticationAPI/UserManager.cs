using AuthenticationAPI.Extensions;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Identity
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
            string loggedInUserName = _httpContextAccessor.HttpContext.User.GetLoggedInUserNameIdentifier();
            var currentUserId = await _userManager.FindByNameAsync(loggedInUserName);

            return currentUserId.Id;
        }

        private async Task<bool> IsCurrentUserAdminRole()
        {
            string loggedInUserName = _httpContextAccessor.HttpContext.User.GetLoggedInUserNameIdentifier();
            var user = await _userManager.FindByNameAsync(loggedInUserName);
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
