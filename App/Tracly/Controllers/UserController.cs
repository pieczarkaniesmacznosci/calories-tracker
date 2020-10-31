using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tracly.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Info()
        {
            return View();
        }
    }
}