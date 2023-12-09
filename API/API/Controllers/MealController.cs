using API.Dtos;
using API.Identity;
using API.Mediator.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize]
    public class MealController : ControllerBase
    {
        private readonly ILogger<MealController> _logger;
        private readonly IUserManager _userManager;
        private readonly IMediator _mediator;
        private int _userId => _userManager.CurrentUserId;
        private bool _isUserAdmin => _userManager.IsCurrentUserAdmin;
        public MealController(
            ILogger<MealController> logger,
            IUserManager userManager,
            IMediator mediator)
        {
            _logger = logger;
            _userManager = userManager;
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
            GetMealByIdQuery query = new() { MealId = id, UserId = _userId };
            MealDto result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("meal")]
        public async Task<IActionResult> CreateMeal(MealDto meal)
        {
            GetMealByIdQuery query = new() { MealId = id, UserId = _userId };
            MealDto result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut]
        [Route("meal")]
        public async Task<IActionResult> EditMeal(MealDto meal)
        {
            GetMealByIdQuery query = new() { MealId = id, UserId = _userId };
            MealDto result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete]
        [Route("meal/{mealId:int}")]
        public async Task<IActionResult> DeleteMeal(int mealId)
        {
            GetMealByIdQuery query = new() { MealId = id, UserId = _userId };
            MealDto result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
