using System.Collections.Generic;

using Tracly.Dtos;
namespace Tracly.ViewModels
{
    public class MealListViewModel
    {
        public string Title { get; set; }
        public List<MealLogDto> MealLogs { get; set; }
        public List<MealDto> SavedMeals { get; set; }
    }
}
