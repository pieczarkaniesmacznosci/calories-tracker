using System.Collections.Generic;
using App.Tracly.Models;

namespace App.Tracly.ViewModels
{
    public class MealListViewModel
    {
        public string Title { get; set; }
        public IEnumerable<Meal> Meals { get; set; }
    }
}