using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Tracly.Dtos;
using Tracly.Extensions;
using Tracly.Models;
using Tracly.ViewModels;

namespace Tracly.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _config { get; }
        private string _apiUrl { get; }
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            _apiUrl = _config["APIUrl"];
        }

        public IActionResult Index()
        {
            var homeVM = new HomeViewModel()
            {
                TodayMealLog = new List<MealLogDto>(),
                UserUntrition = new UserNutritionDto()
            };
            return View(homeVM);
        }

        [HttpGet]
        public async Task<IActionResult> TodaysMealsList()
        {
            var homeVM = new HomeViewModel()
            {
                TodayMealLog = new List<MealLogDto>(),
                UserUntrition = new UserNutritionDto()
            };

            var getMeals = $"{_apiUrl}/meal/todaysMealLog";
            var getUserNutrition = $"{_apiUrl}/user/nutrition";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response;
                var builder = new UriBuilder(getMeals);
                response = await httpClient.GetAsync(builder.ToString());

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    homeVM.TodayMealLog = JsonConvert.DeserializeObject<List<MealLogDto>>(apiResponse);
                }

                HttpResponseMessage userNutritionResponse;
                var userNutritionBuilder = new UriBuilder(getUserNutrition);
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                userNutritionResponse = await httpClient.GetAsync(userNutritionBuilder.ToString());

                if (userNutritionResponse.IsSuccessStatusCode)
                {
                    string apiResponse = await userNutritionResponse.Content.ReadAsStringAsync();
                    homeVM.UserUntrition = JsonConvert.DeserializeObject<UserNutritionDto>(apiResponse);
                }
            }
            return PartialView("_TodaysMealLogTable", homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
