// using API.Web.Entities;
// using Microsoft.AspNet.Identity;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Mvc;

namespace App.Tracly.Controllers
{
    class LoginController
    {
        public LoginController()
        {

        }

        // [HttpPost]
        // public ActionResult Login(LoginViewModel login)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
        //         var authManager = HttpContext.GetOwinContext().Authentication;

        //         User user = userManager.Find(login.UserName, login.Password);
        //         if (user != null)
        //         {
        //             var ident = userManager.CreateIdentity(user,
        //                 DefaultAuthenticationTypes.ApplicationCookie);
        //             //use the instance that has been created. 
        //             authManager.SignIn(
        //                 new AuthenticationProperties { IsPersistent = false }, ident);
        //             return Redirect(login.ReturnUrl ?? Url.Action("Index", "Home"));
        //         }
        //     }
        //     ModelState.AddModelError("", "Invalid username or password");
        //     return View(login);
        // }
    }
}