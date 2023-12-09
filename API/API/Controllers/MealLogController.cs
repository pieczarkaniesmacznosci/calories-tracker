using API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize]
    public class MealLogController : ControllerBase
    {
        [HttpGet]
        [Route("meal/mealsLog")]
        public Task<IActionResult> GetMealLog()
        {
            return base.FromResult(_service.GetMealLog());
        }

        [HttpGet]
        [Route("meal/mealsLog/{mealLogId:int}")]
        public Task<IActionResult> GetMealLog(int mealLogId)
        {
            return base.FromResult(_service.GetMealLog(mealLogId));
        }

        [HttpPost]
        [Route("meal/logMeal")]
        public Task<IActionResult> EatMeal(MealLogDto mealLog)
        {
            return base.FromResult(_service.AddMealLog(mealLog));
        }

        [HttpDelete]
        [Route("meal/logMeal/{mealLogId:int}")]
        public Task<IActionResult> ThrowUp(int mealLogId)
        {
            return base.FromResult(_service.DeleteMealLog(mealLogId));
        }

        [HttpPut]
        [Route("mealLog/{mealLogId:int}/editEaten")]
        public Task<IActionResult> EditEatenMeal(int mealLogId, MealDto meal)
        {
            return base.FromResult(_service.EditMealLog(mealLogId, meal));
        }

        [HttpGet]
        [Route("meal/mealsLog/{date:dateTime}")]
        public async Task<IActionResult> GetMealLog(DateTime date)
        {
            return base.FromResult(_service.GetMealLog(date));
        }

        [HttpGet]
        [Route("meal/todaysMealLog")]
        public async Task<IActionResult> GetTodaysMealLog()
        {
            return base.FromResult(_service.GetMealLog(DateTime.Now.Date));
        }
    }
}
