using API.Dtos;
using API.Identity;
using API.Mediator.Command;
using API.Mediator.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize]
    public class MealController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMediator _mediator;
        private int UserId => _userManager.CurrentUserId;
        public MealController(
            IUserManager userManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("meals")]
        public async Task<IActionResult> GetMeals()
        {
            GetMealsQuery query = new() { UserId = UserId };
            IEnumerable<MealDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("meal/{id}")]
        public async Task<IActionResult> GetMeal(int id)
        {
            GetMealByIdQuery query = new() { MealId = id, UserId = UserId };
            MealDto result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("meals/{mealName}")]
        public async Task<IActionResult> FindProductByName(string mealName)
        {
            GetMealsByNameQuery query = new() { MealName = mealName, UserId = UserId };
            IEnumerable<MealDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("meal")]
        public async Task<IActionResult> CreateMeal(MealDto meal)
        {
            CreateMealCommand command = new() { Meal = meal, UserId = UserId };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Route("meal")]
        public async Task<IActionResult> EditMeal(int mealId, MealDto meal)
        {
            EditMealCommand command = new() { MealId = mealId, Meal = meal, UserId = UserId };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("meal/{mealId:int}")]
        public async Task<IActionResult> DeleteMeal(int mealId)
        {
            DeleteMealCommand command = new() { MealId = mealId, UserId = UserId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
