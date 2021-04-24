using System;
using API.Service;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : BaseController
    {
        private readonly ILogger<MealsController> _logger;
        private readonly IUserService _service;

        public UserController(ILogger<MealsController> logger, IUserService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service;
        }

        [HttpGet]
        [Route("user/weights")]
        public IActionResult GetUserWeights()
        {
            string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            return base.FromResult(_service.GetUserWeights());
        }

        [HttpGet]
        [Route("user/weight")]
        public IActionResult GetCurrentUserWeight()
        {
            return base.FromResult(_service.GetCurrentUserWeight());
        }

        [HttpGet]
        [Route("user/weight/{date}")]
        public IActionResult GetUserWeight(DateTime date)
        {
            return base.FromResult(_service.GetUserWeight(date));
        }

        [HttpPost]
        [Route("user/weight")]
        public IActionResult AddUserWeight(UserWeightDto userWeight)
        {
            return base.FromResult(_service.AddUserWeight(userWeight));
        }

        [HttpPut]
        [Route("user/weight")]
        public IActionResult EditUserWeight(UserWeightDto userWeight)
        {
            return base.FromResult(_service.EditUserWeight(userWeight));
        }

        [HttpDelete]
        [Route("user/weight")]
        public IActionResult DeleteUserWeight(UserWeightDto userWeight)
        {
            return base.FromResult(_service.DeleteUserWeight(userWeight));
        }

        [HttpGet]
        [Route("user/nutritions")]
        public IActionResult GetUserNutritions()
        {
            return base.FromResult(_service.GetUserNutritions());
        }

        [HttpGet]
        [Route("user/nutrition")]
        public IActionResult GetCurrentUserNutrition()
        {
            return base.FromResult(_service.GetCurrentUserNutrition());
        }

        [HttpGet]
        [Route("user/nutrition/{date}")]
        public IActionResult GetUserNutrition(DateTime date)
        {
            return base.FromResult(_service.GetUserNutrition(date));
        }

        [HttpPost]
        [Route("user/nutrition")]
        public IActionResult AddUserNutrition(UserNutritionDto userNutrition)
        {
            return base.FromResult(_service.AddUserNutrition(userNutrition));
        }

        [HttpPut]
        [Route("user/nutrition")]
        public IActionResult EditUserNutrition(UserNutritionDto userNutrition)
        {
            return base.FromResult(_service.EditUserNutrition(userNutrition));
        }

        [HttpDelete]
        [Route("user/nutrition")]
        public IActionResult DeleteUserNutrition(UserNutritionDto userNutrition)
        {
            return base.FromResult(_service.DeleteUserNutrition(userNutrition));
        }
    }
}
