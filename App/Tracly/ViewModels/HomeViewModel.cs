using System.Collections.Generic;

namespace App.Tracly.ViewModels
{
    public class HomeViewModel
    {
        public UserNutritionDto UserUntrition { get; set; }
        public IEnumerable<MealLogDto> TodayMealLog { get; set; }
    }
}