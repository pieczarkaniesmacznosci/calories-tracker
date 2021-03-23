using System.Collections.Generic;
using API.Dtos;

namespace App.Tracly.ViewModels
{
    public class HomeViewModel
    {
        public UserNutritionDto UserUntrition {get;set;}
        public IEnumerable<MealLogDto> TodayMealLog {get;set;}
    }
}