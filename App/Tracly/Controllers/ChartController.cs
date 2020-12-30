using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tracly.Controllers
{
    public class ChartController : Controller
    {
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("UnderConstruction");
        }
    }
}