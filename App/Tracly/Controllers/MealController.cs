using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Tracly.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using API.Web.Dtos;
using Tracly.Models;
using System.Text;
using App.Tracly.ViewModels;
using Tracly.Extensions;
using Microsoft.Extensions.Configuration;

namespace App.Tracly.Controllers
{
    [Authorize]
    public class MealController : Controller
    {
        private readonly ILogger<MealController> _logger;
        private readonly IMealRepository _mealRepository;
        private MealViewModel _viewModel;
        private IConfiguration _config { get; }
        private string _apiUrl{ get; }

        public MealController(ILogger<MealController> logger, IMealRepository mealRepository, IConfiguration configuration)
        {
            _logger = logger;
            _mealRepository = mealRepository;
            _config = configuration;
            _apiUrl = _config["APIUrl"];
        }

        [HttpGet]
        public IActionResult RedirectToList()
        {
            var mealsVM = new MealListViewModel()
            {
                MealLogs = new List<MealLogDto>(),
                SavedMeals = new List<MealDto>()
            };
            return Redirect("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            var mealsVM = new MealListViewModel()
            {
                MealLogs = new List<MealLogDto>(),
                SavedMeals = new List<MealDto>()
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
                var builder = new UriBuilder(getByNamePath);
                builder.Query = $"productName={queryString}";
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
            products ??= new List<ProductDto>();
            return PartialView("_ProductListForMealTable", products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            _viewModel = new MealViewModel();
            _viewModel.MealLog = new MealLogDto(){
                Meal = new MealDto(){
                    MealProducts = new List<MealProductDto>()}
            };
            _viewModel.Products = new List<ProductDto>();
            if(id == null)
            {
                _viewModel.IsEdit= false;
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                    HttpResponseMessage response = await httpClient.GetAsync($"{_apiUrl}/meal/mealsLog/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        _viewModel.MealLog = JsonConvert.DeserializeObject<MealLogDto>(apiResponse);
                        _viewModel.IsEdit= true;
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }

            return View("Details", _viewModel);
        }

        public async Task<MealDto> MealDto(int id)
        {
            var meal = new MealDto(){
                MealProducts = new List<MealProductDto>()
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
                    var builder = new UriBuilder(getByNamePath);
                    builder.Query = $"mealName={queryString}";
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
        public async Task<bool> ProductNameValid(int productId,string productName)
        {
            bool nameIsValid= false;
            using (var httpClient = new HttpClient())
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

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.PostAsync($"{_apiUrl}/meal", stringContent);
            }
        }

        [HttpPost]
        public async void EditEatenMeal(MealLogDto mealLog)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(mealLog.Meal), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.PutAsync($"{_apiUrl}/mealLog/{mealLog?.Id}/editEaten", stringContent);
            }
        }

        [HttpPost]
        public async void EatSavedMeal(MealLogDto mealLog)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(mealLog), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.PostAsync($"{_apiUrl}/meal/logMeal", stringContent);
            }
        }

        [HttpDelete]
        public async void DeleteSavedMeal(int mealId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                var builder = new UriBuilder($"{_apiUrl}/meal/{mealId}");
                HttpResponseMessage response = await httpClient.DeleteAsync(builder.ToString());
            }
        }

        [HttpDelete]
        public async void DeleteConsumedMeal(int mealLogId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                var builder = new UriBuilder($"{_apiUrl}/meal/logMeal/{mealLogId}");
                HttpResponseMessage response = await httpClient.DeleteAsync(builder.ToString());
            }
        }
    }
}