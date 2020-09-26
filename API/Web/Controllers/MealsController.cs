using System;
using API.Web.Service;
using API.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Web.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Route("meal/{id}")]
        public IActionResult GetMeal(int id)
        {
            return base.FromResult(_service.GetMeal(id));
        }

        [HttpGet]
        [Route("meals/{isSaved:bool}")]
        public IActionResult GetMeals(bool isSaved)
        {
            return base.FromResult(_service.GetMeals(isSaved));
        }
        
        [HttpGet]
        [Route("meals/{mealDate:dateTime}")]
        public IActionResult FindMealsByDate(DateTime mealDate)
        {
            return base.FromResult(_service.GetMeals(mealDate));
        }

        [HttpPost]
        [Route("meal")]
        public IActionResult AddMeal(MealDto meal)
        {
            return base.FromResult(_service.AddMeal(meal));
        }

        [HttpPut]
        [Route("meal")]
        public IActionResult EditMeal(MealDto meal)
        {
            return base.FromResult(_service.EditMeal(meal));
        }
        
        [HttpDelete]
        [Route("meal")]
        public IActionResult DeleteMeal(int id)
        {
            return base.FromResult(_service.DeleteMeal(id));
        }
    }
}
