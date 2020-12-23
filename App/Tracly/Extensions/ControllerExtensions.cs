using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

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