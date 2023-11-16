using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Tracly.Extensions
{
    public static class ControllerExtensions
    {
        public static AuthenticationHeaderValue AddAuthenticationToken(this HttpRequest controller)
        {
            var tokenCookie = controller.Cookies["X-Access-Token"];
            return new AuthenticationHeaderValue("Bearer", tokenCookie);
        }
    }
}