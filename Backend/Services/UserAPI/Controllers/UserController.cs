using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        //private readonly IUserManager _userManager;
        //private readonly IUserService _service;
        //private int UserId => _userManager.CurrentUserId;

        //public UserController(
        //    IUserManager userManager,
        //    IUserService service)
        //{
        //    _userManager = userManager;
        //    _service = service;
        //}

        //[HttpGet]
        //[Route("weights")]
        //public async Task<IActionResult> GetUserWeights()
        //{
        //    IEnumerable<UserWeightDto> userWeights = await _service.GetUserWeightsAsync(UserId);
        //    return Ok(userWeights);
        //}

        //[HttpGet]
        //[Route("currentweight")]
        //public async Task<IActionResult> GetUserCurrentWeight()
        //{
        //    UserWeightDto userWeight = await _service.GetUserWeightAsync(UserId);
        //    return Ok(userWeight);
        //}

        //[HttpGet]
        //[Route("weight/{date}")]
        //public async Task<IActionResult> GetUserWeight(DateTime date)
        //{
        //    UserWeightDto userWeight = await _service.GetUserWeightAsync(UserId, date);
        //    return Ok(userWeight);
        //}

        //[HttpPost]
        //[Route("weight")]
        //public async Task<IActionResult> AddUserWeight(UserWeightDto userWeight)
        //{
        //    userWeight.UserId = UserId;
        //    await _service.AddUserWeightAsync(userWeight);
        //    return Ok(userWeight);
        //}

        //[HttpPut]
        //[Route("weight")]
        //public async Task<IActionResult> EditUserWeight(UserWeightDto userWeight)
        //{
        //    userWeight.UserId = UserId;
        //    await _service.EditUserWeightAsync(userWeight);
        //    return Ok(userWeight);
        //}

        //[HttpDelete]
        //[Route("weight")]
        //public async Task<IActionResult> DeleteUserWeight(int userWeightId)
        //{
        //    await _service.DeleteUserWeightAsync(UserId, userWeightId);
        //    return Ok();
        //}

        //[HttpGet]
        //[Route("nutritions")]
        //public async Task<IActionResult> GetUserNutritions()
        //{
        //    IEnumerable<UserNutritionDto> result = await _service.GetUserNutritionsAsync(UserId);
        //    return Ok(result);
        //}

        //[HttpGet]
        //[Route("currentnutrition")]
        //public async Task<IActionResult> GetUserCurrentNutrition()
        //{
        //    UserNutritionDto result = await _service.GetUserCurrentNutritionAsync(UserId);
        //    return Ok(result);
        //}

        //[HttpGet]
        //[Route("nutrition/{date}")]
        //public async Task<IActionResult> GetUserNutrition(DateTime date)
        //{
        //    UserNutritionDto result = await _service.GetUserNutritionAsync(UserId, date);
        //    return Ok(result);
        //}

        //[HttpPost]
        //[Route("nutrition")]
        //public async Task<IActionResult> AddUserNutrition(UserNutritionDto userNutrition)
        //{
        //    await _service.AddUserNutritionAsync(UserId, userNutrition);
        //    return Ok();
        //}

        //[HttpPut]
        //[Route("nutrition")]
        //public async Task<IActionResult> EditUserNutrition(UserNutritionDto userNutrition)
        //{
        //    await _service.EditUserNutritionAsync(UserId, userNutrition);
        //    return Ok();
        //}

        //[HttpDelete]
        //[Route("nutrition")]
        //public async Task<IActionResult> DeleteUserNutrition(int userNutritionId)
        //{
        //    await _service.DeleteUserNutritionAsync(UserId, userNutritionId);
        //    return Ok();
        //}
    }
}
