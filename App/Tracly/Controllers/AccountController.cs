using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.Web.Entities;
using App.Tracly.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Web.Dtos;

namespace App.Tracly.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// The manager for handling user creation, deletion, searching, roles etc...
        /// </summary>
        protected UserManager<User> _userManager;

        /// <summary>
        /// The manager for handling signing in and out for our users
        /// </summary>
        protected SignInManager<User> _signInManager;

        public AccountController(
            ILogger<AccountController> logger,
            IUserRepository userRepository,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "/Home/Index")
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberLogin, false);

                if (result.Succeeded)
                {
                    var content = new StringContent(JsonConvert.SerializeObject( new {Login = model.UserName, Password=model.Password}), Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        HttpResponseMessage response = await httpClient.PostAsync("http://localhost:5005/api/token", content);

                        var tokenDto = JsonConvert.DeserializeObject<ResponseTokenDto>(response.Content.ReadAsStringAsync().Result);
                        Response.Cookies.Append("X-Access-Token", tokenDto.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    }

                    return LocalRedirect(model.ReturnUrl);// used local insterd od simple redirect to avoid open redirection attacks
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("X-Access-Token");
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
    }

// public static class HttpExtensions
//     {
//         public static async Task HandleToken(this HttpClient client, string authority, string clientId, string secret, string apiName)
//         {
//             var accessToken = await client.GetRefreshTokenAsync(authority, clientId, secret, apiName);
//             client.SetBearerToken(accessToken);
//         }

//         private static async Task<string> GetRefreshTokenAsync(this HttpClient client, string authority, string clientId, string secret, string apiName)
//         {
//             var disco = await client.GetDiscoveryDocumentAsync(authority);
//             if (disco.IsError) throw new Exception(disco.Error);

//             var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
//             {
//                 Address = disco.TokenEndpoint,
//                 ClientId = clientId,
//                 ClientSecret = secret,
//                 Scope=apiName
//             });

//             if (!tokenResponse.IsError) return tokenResponse.AccessToken;
//             return null;
//         }
//     }
}