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
    public class MealLogController : ControllerBase
    {
        private readonly IMediator _mediator;
        private Guid _userId => Request.HttpContext.User.GetLoggedInUserSub();
        public MealLogController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetMealLogs()
        {
            GetMealLogsQuery query = new() { UserId = _userId };
            IEnumerable<MealLogDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMealLog([FromQuery] int mealLogId)
        {
            GetMealLogByIdQuery query = new() { UserId = _userId, MealLogId = mealLogId };
            MealLogDto result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("{date:dateTime}")]
        public async Task<IActionResult> GetMealLogByDate([FromQuery] DateTime date)
        {
            GetMealLogsByDateQuery query = new() { UserId = _userId, MealLogDate = date };
            IEnumerable<MealLogDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMealLog(MealLogDto mealLog)
        {
            CreateMealLogCommand command = new() { UserId = _userId, MealLog = mealLog };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Route("{mealLogId}")]
        public async Task<IActionResult> EditMealLog(int mealLogId, MealLogDto mealLog)
        {
            EditMealLogCommand command = new() { UserId = _userId, MealLogId = mealLogId, MealLog = mealLog };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("{mealLogId}")]
        public async Task<IActionResult> DeleteMealLog(int mealLogId)
        {
            DeleteMealLogCommand command = new() { UserId = _userId, MealLogId = mealLogId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
