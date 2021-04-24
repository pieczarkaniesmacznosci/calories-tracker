using System;
using API.Service;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Data.Entities;

namespace API.Controllers
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
        [Route("meals/mealsByName")]
        public IActionResult FindProductByName(string mealName)
        {
            return base.FromResult(_service.GetMeals(mealName));
        }

        [HttpGet]
        [Route("meal/{id}/nameValid")]
        public IActionResult MealNameValid(int id, string mealName)
        {
            return base.FromResult(_service.MealNameValid(id,mealName));
        }

        [HttpPost]
        [Route("meal")]
        public IActionResult AddMeal(MealDto meal)
        {
            return base.FromResult(_service.AddMeal(meal));
        }

        [HttpGet]
        [Route("meal/mealsLog")]
        public IActionResult GetMealLog()
        {
            return base.FromResult(_service.GetMealLog());
        }

        [HttpGet]
        [Route("meal/mealsLog/{mealLogId:int}")]
        public IActionResult GetMealLog(int mealLogId)
        {
            return base.FromResult(_service.GetMealLog(mealLogId));
        }

        [HttpGet]
        [Route("meal/mealsLog/{date:dateTime}")]
        public IActionResult GetMealLog(DateTime date)
        {
            return base.FromResult(_service.GetMealLog(date));
        }

        [HttpGet]
        [Route("meal/todaysMealLog")]
        public IActionResult GetTodaysMealLog()
        {
            return base.FromResult(_service.GetMealLog(DateTime.Now.Date));
        }

        [HttpPost]
        [Route("meal/logMeal")]
        public IActionResult EatMeal(MealLogDto mealLog)
        {
            return base.FromResult(_service.AddMealLog(mealLog));
        }

        [HttpDelete]
        [Route("meal/logMeal/{mealLogId:int}")]
        public IActionResult ThrowUp(int mealLogId)
        {
            return base.FromResult(_service.DeleteMealLog(mealLogId));
        }

        [HttpPut]
        [Route("mealLog/{mealLogId:int}/editEaten")]
        public IActionResult EditEatenMeal(int mealLogId,MealDto meal)
        {
            return base.FromResult(_service.EditEatenMeal(mealLogId,meal));
        }

        [HttpPut]
        [Route("meal")]
        public IActionResult EditMeal(MealDto meal)
        {
            return base.FromResult(_service.EditMeal(meal));
        }
        
        [HttpDelete]
        [Route("meal/{mealId:int}")]
        public IActionResult DeleteMeal(int mealId)
        {
            return base.FromResult(_service.DeleteMeal(mealId));
        }
    }
}
