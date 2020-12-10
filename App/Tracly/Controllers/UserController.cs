using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using App.Tracly.ViewModels;
using API.Web.Dtos;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace App.Tracly.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Info()
        {
            var userVM = new UserViewModel()
            {
                Weight = new UserWeightDto(),
                Nutrition = new UserNutritionDto()
            };
            return View(userVM);
        }

        [HttpGet]
        public async Task<UserWeightDto> UserWeight()
        {
            var weight = new UserWeightDto();
            var getUserWeight = $"http://localhost:5005/api/user/weight";
            
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage weightResponse;
                var builderWeight = new UriBuilder(getUserWeight);

                weightResponse = await httpClient.GetAsync(builderWeight.ToString());
                if (weightResponse.IsSuccessStatusCode)
                {
                    string apiResponse = await weightResponse.Content.ReadAsStringAsync();
                    weight = JsonConvert.DeserializeObject<UserWeightDto>(apiResponse);
                }
            }
            return weight;
        }

        [HttpGet]
        public async Task<UserNutritionDto> UserNutrition()
        {
            var nutrition = new UserNutritionDto();
            var getUserWeight = $"http://localhost:5005/api/user/nutrition";
            
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage weightResponse;
                var builderWeight = new UriBuilder(getUserWeight);

                weightResponse = await httpClient.GetAsync(builderWeight.ToString());
                if (weightResponse.IsSuccessStatusCode)
                {
                    string apiResponse = await weightResponse.Content.ReadAsStringAsync();
                    nutrition = JsonConvert.DeserializeObject<UserNutritionDto>(apiResponse);
                }
            }
            return nutrition;
        }

        [HttpPost]
        public async Task<UserWeightDto> PostUserWeight(UserWeightDto userWeight)
        {
            var PostUserWeight = $"http://localhost:5005/api/user/nutrition";
            
            using (var httpClient = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(userWeight), Encoding.UTF8, "application/json");

                var builderWeight = new UriBuilder(PostUserWeight);

                var response = await httpClient.PostAsync(builderWeight.ToString(),stringContent);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userWeight = JsonConvert.DeserializeObject<UserWeightDto>(apiResponse);
                }
            }
            return userWeight;
        }

        [HttpPost]
        public async Task<UserNutritionDto> PostUserNutrition(UserNutritionDto userNutrition)
        {
            var postUserNutrition = $"http://localhost:5005/api/user/nutrition";
            
            using (var httpClient = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(userNutrition), Encoding.UTF8, "application/json");

                var builderWeight = new UriBuilder(postUserNutrition);

                var response = await httpClient.PostAsync(builderWeight.ToString(),stringContent);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userNutrition = JsonConvert.DeserializeObject<UserNutritionDto>(apiResponse);
                }
            }
            return userNutrition;
        }
    }
}