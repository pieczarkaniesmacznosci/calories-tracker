using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Tracly.Models;
using App.Tracly.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using API.Web.Entities;

namespace App.Tracly.Controllers
{
    [Authorize]
    public class MealController : Controller
    {
        private readonly ILogger<MealController> _logger;
        private readonly IMealRepository _mealRepository;

        public MealController(ILogger<MealController> logger, IMealRepository mealRepository)
        {
            _logger = logger;
            _mealRepository = mealRepository;
        }

        public async Task<IActionResult> ListAsync()
        {
            var mealListVM = new MealListViewModel();

            mealListVM.Title = "Meals";

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5005/api/meal");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    mealListVM.Meals = JsonConvert.DeserializeObject<List<Meal>>(apiResponse);
                }
            }
            return View(mealListVM);
        }

        public IActionResult Details(int id)
        {
            var meal = _mealRepository.MealById(id);

            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }
    }
}