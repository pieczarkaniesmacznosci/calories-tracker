using CaloriesAPI.Dtos;
using CaloriesAPI.Mediator.Command;
using CaloriesAPI.Mediator.Query;
using Common.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MealController : ControllerBase
    {
        private readonly IMediator _mediator;
        private Guid _userId => Request.HttpContext.User.GetLoggedInUserSub();
        public MealController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetMeals()
        {
            GetMealsQuery query = new() { UserId = _userId };
            IEnumerable<MealDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("{mealId}")]
        public async Task<IActionResult> GetMealById(int mealId)
        {
            GetMealByIdQuery query = new() { MealId = mealId, UserId = _userId };
            MealDto result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FindMealByName([FromQuery] string mealName)
        {
            GetMealsByNameQuery query = new() { MealName = mealName, UserId = _userId };
            IEnumerable<MealDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeal(MealDto meal)
        {
            CreateMealCommand command = new() { Meal = meal, UserId = _userId };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Route("{mealId}")]
        public async Task<IActionResult> EditMeal(int mealId, MealDto meal)
        {
            EditMealCommand command = new() { MealId = mealId, Meal = meal, UserId = _userId };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("{mealId:int}")]
        public async Task<IActionResult> DeleteMeal(int mealId)
        {
            DeleteMealCommand command = new() { MealId = mealId, UserId = _userId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
