using System.Threading.Tasks;
using API.Web.Entities;
using App.Tracly.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
                    return LocalRedirect(model.ReturnUrl);// used local insterd od simple redirect to avoid open redirection attacks
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}