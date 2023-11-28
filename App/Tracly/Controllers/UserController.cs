using App.Tracly.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tracly.Dtos;
using Tracly.Extensions;

namespace App.Tracly.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private IConfiguration _config { get; }
        private string _apiUrl { get; }
        public UserController(ILogger<UserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            _apiUrl = _config["APIUrl"];
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
            var getUserWeight = $"{_apiUrl}/user/weight";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
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
            var getUserWeight = $"{_apiUrl}/user/nutrition";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
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
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.PostAsync($"{_apiUrl}/user/weight", stringContent);
            }
        }

        [HttpPost]
        public async void PostUserNutrition(UserNutritionDto userNutrition)
        {
            var postUserNutrition = $"{_apiUrl}/user/nutrition";

            userNutrition.Date = DateTime.Now;
            var stringContent = new StringContent(JsonConvert.SerializeObject(userNutrition), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.PostAsync(postUserNutrition, stringContent);
            }
        }
    }
}
