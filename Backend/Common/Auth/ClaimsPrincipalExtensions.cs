using System.Security.Claims;

namespace Common.Auth
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetLoggedInUserSub(this ClaimsPrincipal principal)
        {
            ArgumentNullException.ThrowIfNull(principal);

            var userSub = principal.FindFirstValue(ClaimTypes.NameIdentifier.ToString());
            ArgumentNullException.ThrowIfNull(userSub);

            return Guid.Parse(userSub);
        }
    }
}
