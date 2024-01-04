using Microsoft.AspNetCore.Mvc;

namespace Tracly.Controllers
{
    public class ChartController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View("UnderConstruction");
        }
    }
}
