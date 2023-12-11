﻿using API.Dtos;
using API.Identity;
using API.Mediator.Command;
using API.Mediator.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize]
    public class MealLogController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMediator _mediator;
        private int _userId => _userManager.CurrentUserId;
        public MealLogController(
            IUserManager userManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("mealLogs")]
        public async Task<IActionResult> GetMealLogs()
        {
            GetMealLogsQuery query = new() { UserId = _userId };
            IEnumerable<MealLogDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("{mealLogId:int}")]
        public async Task<IActionResult> GetMealLog(int mealLogId)
        {
            GetMealLogByIdQuery query = new() { UserId = _userId, MealLogId = mealLogId };
            MealLogDto result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("{date:dateTime}")]
        public async Task<IActionResult> GetMealLog(DateTime date)
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
        public async Task<IActionResult> EditMealLog(int mealLogId, MealLogDto mealLog)
        {
            EditMealLogCommand command = new() { UserId = _userId, MealLogId = mealLogId, MealLog = mealLog };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMealLog(int mealLogId)
        {
            DeleteMealLogCommand command = new() { UserId = _userId, MealLogId = mealLogId };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
