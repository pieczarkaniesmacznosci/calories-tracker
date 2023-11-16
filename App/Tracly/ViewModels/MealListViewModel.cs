using System.Collections.Generic;

namespace App.Tracly.ViewModels
{
    public class MealListViewModel
    {
        public string Title { get; set; }
        public List<MealLogDto> MealLogs { get; set; }
        public List<MealDto> SavedMeals { get; set; }
    }
}