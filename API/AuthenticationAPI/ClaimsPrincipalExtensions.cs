using System.Security.Claims;

namespace AuthenticationAPI.Extensions
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
