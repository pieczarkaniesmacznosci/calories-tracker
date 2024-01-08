using AuthenticationAPI.Extensions;
using CaloriesAPI.Dtos;
using CaloriesAPI.Mediator.Command;
using CaloriesAPI.Mediator.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize]
    public class MealController : ControllerBase
    {
        private readonly IMediator _mediator;
        private int _userId => Request.HttpContext.User.GetLoggedInUserSub();
        public MealController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("meals")]
        public async Task<IActionResult> GetMeals()
        {
            GetMealsQuery query = new() { UserId = _userId };
            IEnumerable<MealDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("meal/{id}")]
        public async Task<IActionResult> GetMeal(int id)
        {
            GetMealByIdQuery query = new() { MealId = id, UserId = _userId };
            MealDto result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("meals/{mealName}")]
        public async Task<IActionResult> FindProductByName(string mealName)
        {
            GetMealsByNameQuery query = new() { MealName = mealName, UserId = _userId };
            IEnumerable<MealDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("meal")]
        public async Task<IActionResult> CreateMeal(MealDto meal)
        {
            CreateMealCommand command = new() { Meal = meal, UserId = _userId };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Route("meal")]
        public async Task<IActionResult> EditMeal(int mealId, MealDto meal)
        {
            EditMealCommand command = new() { MealId = mealId, Meal = meal, UserId = _userId };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("meal/{mealId:int}")]
        public async Task<IActionResult> DeleteMeal(int mealId)
        {
            DeleteMealCommand command = new() { MealId = mealId, UserId = _userId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}