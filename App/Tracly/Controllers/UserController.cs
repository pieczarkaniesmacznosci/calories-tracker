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
        public async void PostUserWeight(UserWeightDto userWeight)
        {
            userWeight.Date = DateTime.Now;
            var stringContent = new StringContent(JsonConvert.SerializeObject(userWeight), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync("http://localhost:5005/api/user/weight", stringContent);
            }
        }

        [HttpPost]
        public async void PostUserNutrition(UserNutritionDto userNutrition)
        {
            var postUserNutrition = $"http://localhost:5005/api/user/nutrition";
            
            userNutrition.Date = DateTime.Now;
            var stringContent = new StringContent(JsonConvert.SerializeObject(userNutrition), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync(postUserNutrition, stringContent);
            }
        }
    }
}