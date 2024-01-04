using CaloriesAPI.Dtos;
using CaloriesAPI.Mediator.Command;
using CaloriesAPI.Mediator.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesAPI.Controllers
{
    [ApiController]
    [Route("api/mealLog")]
    [Authorize]
    public class MealLogController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MealLogController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("mealLogs")]
        public async Task<IActionResult> GetMealLogs()
        {
            GetMealLogsQuery query = new() { UserId = 1 };
            IEnumerable<MealLogDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("{mealLogId:int}")]
        public async Task<IActionResult> GetMealLog(int mealLogId)
        {
            GetMealLogByIdQuery query = new() { UserId = 1, MealLogId = mealLogId };
            MealLogDto result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("{date:dateTime}")]
        public async Task<IActionResult> GetMealLog(DateTime date)
        {
            GetMealLogsByDateQuery query = new() { UserId = 1, MealLogDate = date };
            IEnumerable<MealLogDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMealLog(MealLogDto mealLog)
        {
            CreateMealLogCommand command = new() { UserId = 1, MealLog = mealLog };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditMealLog(int mealLogId, MealLogDto mealLog)
        {
            EditMealLogCommand command = new() { UserId = 1, MealLogId = mealLogId, MealLog = mealLog };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMealLog(int mealLogId)
        {
            DeleteMealLogCommand command = new() { UserId = 1, MealLogId = mealLogId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
