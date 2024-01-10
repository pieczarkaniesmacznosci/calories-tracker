using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tracly.Dtos;
using Tracly.Extensions;
using Tracly.Models;
using Tracly.ViewModels;

namespace Tracly.Controllers
{
    [Authorize]
    public class MealController : Controller
    {
        private readonly ILogger<MealController> _logger;
        private MealViewModel _viewModel;
        private IConfiguration _config { get; }
        private string _apiUrl { get; }

        public MealController(ILogger<MealController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            _apiUrl = _config["APIUrl"];
        }

        [HttpGet]
        public IActionResult RedirectToList()
        {
            return Redirect("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            MealListViewModel mealsVM = new()
            {
                MealLogs = [],
                SavedMeals = []
            };
            return View("List", mealsVM);
        }

        [HttpGet]
        public async Task<IActionResult> ConsumedMealsList()
        {
            var meals = new List<MealLogDto>();
            var getMeals = $"{_apiUrl}/meal/mealsLog";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response;
                var builder = new UriBuilder(getMeals);
                response = await httpClient.GetAsync(builder.ToString());

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    meals = JsonConvert.DeserializeObject<List<MealLogDto>>(apiResponse);
                }
            }
            return PartialView("_ConsumedMealsList", meals);
        }

        [HttpGet]
        public async Task<IActionResult> SavedMealsList()
        {
            var meals = new List<MealDto>();
            var getMeals = $"{_apiUrl}/meals/true";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response;
                var builder = new UriBuilder(getMeals);
                response = await httpClient.GetAsync(builder.ToString());

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    meals = JsonConvert.DeserializeObject<List<MealDto>>(apiResponse);
                }
            }
            return PartialView("_SavedMealsList", meals);
        }

        [HttpGet]
        public async Task<List<ProductDto>> ProductsForMeal(string queryString)
        {
            var products = new List<ProductDto>();
            var getByNamePath = $"{_apiUrl}/products/name";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response;
                UriBuilder builder = new(getByNamePath)
                {
                    Query = $"productName={queryString}"
                };
                response = await httpClient.GetAsync(builder.ToString());

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ProductDto>>(apiResponse);
                }
            }
            return products;
        }

        [HttpPost]
        public IActionResult ProductForMealTable(List<ProductDto> products)
        {
            products ??= [];
            return PartialView("_ProductListForMealTable", products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            _viewModel = new MealViewModel
            {
                MealLog = new()
                {
                    Meal = new()
                    {
                        MealProducts = []
                    }
                },
                Products = []
            };
            if (id == null)
            {
                _viewModel.IsEdit = false;
            }
            else
            {
                using HttpClient httpClient = new();
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.GetAsync($"{_apiUrl}/meal/mealsLog/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _viewModel.MealLog = JsonConvert.DeserializeObject<MealLogDto>(apiResponse);
                    _viewModel.IsEdit = true;
                }
                else
                {
                    return NotFound();
                }
            }

            return View("Details", _viewModel);
        }

        public async Task<MealDto> MealDto(int id)
        {
            MealDto meal = new()
            {
                MealProducts = []
            };

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.GetAsync($"{_apiUrl}/meal/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    meal = JsonConvert.DeserializeObject<MealDto>(apiResponse);
                }
            }
            return meal;
        }

        [HttpGet]
        public async Task<IActionResult> MealListTable(string queryString)
        {
            var meals = new List<MealDto>();
            var getPath = $"{_apiUrl}/meals/true";
            var getByNamePath = $"{_apiUrl}/meals/mealsByName";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response;
                if (!string.IsNullOrWhiteSpace(queryString))
                {
                    UriBuilder builder = new(getByNamePath)
                    {
                        Query = $"mealName={queryString}"
                    };
                    response = await httpClient.GetAsync(builder.ToString());
                }
                else
                {
                    response = await httpClient.GetAsync(getPath);
                }
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    meals = JsonConvert.DeserializeObject<List<MealDto>>(apiResponse);
                }
            }
            return PartialView("_SavedMealsList", meals);
        }

        [HttpPost]
        [Route("MealNameValid")]
        public async Task<bool> ProductNameValid(int productId, string productName)
        {
            bool nameIsValid = false;
            using (HttpClient httpClient = new())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.GetAsync($"{_apiUrl}/product/{productId}/nameValid?productName={productName}");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    nameIsValid = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }
            return nameIsValid;
        }

        [HttpPost]
        public IActionResult GenerateMealProductListTable(List<MealProductDto> mealProducts)
        {
            return PartialView("_MealProductsList", mealProducts);
        }

        [HttpPost]
        public async void PostMeal(MealDto meal)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(meal), Encoding.UTF8, "application/json");

            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
            HttpResponseMessage response = await httpClient.PostAsync($"{_apiUrl}/meal", stringContent);
        }

        [HttpPost]
        public async void EditEatenMeal(MealLogDto mealLog)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(mealLog.Meal), Encoding.UTF8, "application/json");

            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
            HttpResponseMessage response = await httpClient.PutAsync($"{_apiUrl}/mealLog/{mealLog?.Id}/editEaten", stringContent);
        }

        [HttpPost]
        public async void EatSavedMeal(MealLogDto mealLog)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(mealLog), Encoding.UTF8, "application/json");

            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
            HttpResponseMessage response = await httpClient.PostAsync($"{_apiUrl}/meal/logMeal", stringContent);
        }

        [HttpDelete]
        public async void DeleteSavedMeal(int mealId)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
            var builder = new UriBuilder($"{_apiUrl}/meal/{mealId}");
            HttpResponseMessage response = await httpClient.DeleteAsync(builder.ToString());
        }

        [HttpDelete]
        public async void DeleteConsumedMeal(int mealLogId)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
            var builder = new UriBuilder($"{_apiUrl}/meal/logMeal/{mealLogId}");
            HttpResponseMessage response = await httpClient.DeleteAsync(builder.ToString());
        }
    }
}
