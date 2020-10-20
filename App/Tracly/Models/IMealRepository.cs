using System.Collections.Generic;

namespace Tracly.Models
{
    public interface IMealRepository
    {
        IEnumerable<Meal> AllMeals {get;}
        Meal MealById(int mealId);
    }
}