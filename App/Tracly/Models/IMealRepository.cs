using System.Collections.Generic;
using API.Web.Entities;

namespace App.Tracly.Models
{
    public interface IMealRepository
    {
        IEnumerable<Meal> AllMeals { get; }
        Meal MealById(int mealId);
    }
}