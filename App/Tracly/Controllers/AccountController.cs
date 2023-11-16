using App.Tracly.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Tracly.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserRepository _userRepository;
        private IConfiguration _config { get; }
        private string _apiUrl { get; }

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
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
            _apiUrl = _config["APIUrl"];

        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "/Home/Index")
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
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
                    var content = new StringContent(JsonConvert.SerializeObject(new { Login = model.UserName, Password = model.Password }), Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        HttpResponseMessage response = await httpClient.PostAsync($"{_apiUrl}/token", content);

                        var tokenDto = JsonConvert.DeserializeObject<ResponseTokenDto>(response.Content.ReadAsStringAsync().Result);
                        Response.Cookies.Append("X-Access-Token", tokenDto.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    }

                    return LocalRedirect(model.ReturnUrl);// used local insterd od simple redirect to avoid open redirection attacks
                }
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    user = new User
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    if (result.Succeeded)
                    {
                        var resultRole = await _userManager.AddToRoleAsync(user, "User");

                        foreach (var error in resultRole.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        if (resultRole.Succeeded)
                        {
                            return LocalRedirect("/Home/Index");
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("Email", "User with supplied e-mail address already exists!");
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("X-Access-Token");
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}
