using System.Collections.Generic;

namespace App.Tracly.Models
{
    public interface IMealRepository
    {
        IEnumerable<Meal> AllMeals { get; }
        Meal MealById(int mealId);
    }
}