using System.Security.Claims;

namespace UserService.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetLoggedInUserNameIdentifier(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
