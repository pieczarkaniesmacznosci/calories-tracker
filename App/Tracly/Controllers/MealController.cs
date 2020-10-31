using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Tracly.Models;
using App.Tracly.ViewModels;

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

        public IActionResult List()
        {
            var mealListVM = new MealListViewModel();

            mealListVM.Title = "Meals";
            mealListVM.Meals = _mealRepository.AllMeals;

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