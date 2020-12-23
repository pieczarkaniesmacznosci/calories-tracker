using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Tracly.Models;
using Microsoft.AspNetCore.Authorization;
using App.Tracly.ViewModels;
using System.Collections.Generic;
using API.Web.Dtos;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Tracly.Extensions;

namespace App.Tracly.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var homeVM = new HomeViewModel(){
                TodayMealLog = new List<MealLogDto>(),
                UserUntrition = new UserNutritionDto()
            };
            return View(homeVM);
        }

        [HttpGet]
        public async Task<IActionResult> TodaysMealsList()
        {
            var homeVM = new HomeViewModel(){
                TodayMealLog = new List<MealLogDto>(),
                UserUntrition = new UserNutritionDto()
            };
            var mealLog = new List<MealLogDto>();
            var getMeals = $"http://localhost:5005/api/meal/todaysMealLog";
            var getUserNutrition = $"http://localhost:5005/api/user/nutrition";
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
