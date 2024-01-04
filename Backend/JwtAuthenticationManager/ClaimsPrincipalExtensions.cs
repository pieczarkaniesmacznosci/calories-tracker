using System.Security.Claims;

namespace AuthenticationAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetLoggedInUserSub(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
        }
    }
}
