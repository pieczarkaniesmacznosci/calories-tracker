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
using System.Linq;

namespace App.Tracly.Controllers
{
    [Authorize]
    public class MealController : Controller
    {
        private readonly ILogger<MealController> _logger;
        private readonly IMealRepository _mealRepository;
        private MealViewModel _viewModel;

        public MealController(ILogger<MealController> logger, IMealRepository mealRepository)
        {
            _logger = logger;
            _mealRepository = mealRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var meals = new List<MealDto>();

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5005/api/meals");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    meals = JsonConvert.DeserializeObject<List<MealDto>>(apiResponse);
                }
            }
            return View("List", meals);
        }

        [HttpGet]
        public async Task<IActionResult> MealsList(bool saved)
        {
            var meals = new List<MealDto>();
            var getMeals = $"http://localhost:5005/api/meals/{saved}";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response;
                var builder = new UriBuilder(getMeals);
                response = await httpClient.GetAsync(builder.ToString());
                
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    meals = JsonConvert.DeserializeObject<List<MealDto>>(apiResponse);
                }
            }
            return PartialView("_MealsListItem", meals);
        }
        public async Task<IActionResult> ListAsync()
        {
            var mealsList = new List<MealDto>();
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5005/api/meal");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    mealsList = JsonConvert.DeserializeObject<List<MealDto>>(apiResponse);
                }
            }
            return View(mealsList);
        }

        public async Task<IActionResult> Details(int id)
        {
            _viewModel = new MealViewModel();
            _viewModel.Meal = new MealDto();
            _viewModel.Products = new List<ProductDto>();

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5005/api/meal/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _viewModel.Meal = JsonConvert.DeserializeObject<MealDto>(apiResponse);

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
            var meal = new MealDto();

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5005/api/meal/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    meal = JsonConvert.DeserializeObject<MealDto>(apiResponse);

                }
            }
            return meal;
        }

        [HttpPost]
        public IActionResult GenerateMealProductListTable(List<MealProductDto> mealProducts)
        {
            return PartialView("_MealProductListTable", mealProducts);
        }

        [HttpGet]
        public async Task<IActionResult> ProductsList(string queryString)
        {
            var products = new List<ProductDto>();
            var getPath = "http://localhost:5005/api/products";
            var getByNamePath = "http://localhost:5005/api/products/name";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response;
                if (!string.IsNullOrWhiteSpace(queryString))
                {
                    var builder = new UriBuilder(getByNamePath);
                    builder.Query = $"productName={queryString}";
                    response = await httpClient.GetAsync(builder.ToString());
                }
                else
                {
                    response = await httpClient.GetAsync(getPath);
                }
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ProductDto>>(apiResponse);
                }
            }
            return PartialView("_ProductListItemMeal", products);
        }
    }
}