using System.Security.Claims;

namespace AuthenticationAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetLoggedInUserSub(this ClaimsPrincipal principal)
        {
            ArgumentNullException.ThrowIfNull(principal);

            var userSub = principal.FindFirstValue(ClaimTypes.NameIdentifier.ToString());
            ArgumentNullException.ThrowIfNull(userSub);

            return int.Parse(userSub);
        }
    }
}
