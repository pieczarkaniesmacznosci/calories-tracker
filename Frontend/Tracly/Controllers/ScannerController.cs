using Microsoft.AspNetCore.Mvc;

namespace Tracly.Controllers
{
    public class ScannerController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("UnderConstruction");
        }
    }
}