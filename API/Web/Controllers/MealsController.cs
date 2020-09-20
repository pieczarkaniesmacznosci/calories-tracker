using System;
using API.Web.Service;
using API.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Web.Controllers
{
    [ApiController]
    [Route("api/meals")]
    public class MealsController : BaseController
    {
        private readonly ILogger<MealsController> _logger;
        private readonly IMealService _service;

        public MealsController(ILogger<MealsController> logger, IMealService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service;
        }

        [HttpGet]
        [Route("id/{id}")]
        public IActionResult GetMeal(int id)
        {
            return base.FromResult(_service.GetMeal(id));
        }

        [HttpGet]
        public IActionResult GetMeals(int userId)
        {
            return base.FromResult(_service.GetMeals());
        }
        
        [HttpGet]
        [Route("name/{mealDate}")]
        public IActionResult FindMealByDate(DateTime mealDate)
        {
            return base.FromResult(_service.GetMeals(mealDate));
        }

        [HttpPost]
        public IActionResult AddMeal(MealDto meal)
        {
            return base.FromResult(_service.AddMeal(meal));
        }

        [HttpPut]
        public IActionResult EditMeal(MealDto meal)
        {
            return base.FromResult(_service.EditMeal(meal));
        }
        
        [HttpDelete]
        public IActionResult DeleteMeal(int id)
        {
            return base.FromResult(_service.DeleteMeal(id));
        }
    }
}
