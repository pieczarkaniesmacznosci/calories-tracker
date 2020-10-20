using System.Collections.Generic;
using Tracly.Models;

namespace Tracly.ViewModels
{
    public class MealListViewModel
    {
        public string Title { get; set; }
        public IEnumerable<Meal> Meals {get;set;}
    }
}