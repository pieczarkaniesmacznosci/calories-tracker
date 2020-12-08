using System.Collections.Generic;
using API.Web.Dtos;

namespace App.Tracly.ViewModels
{
    public class MealListViewModel
    {
        public string Title { get; set; }
        public List<MealLogDto> MealLogs {get;set;}
        public List<MealDto> SavedMeals {get;set;}
    }
}