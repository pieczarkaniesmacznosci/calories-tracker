using System;
using System.Collections.Generic;
using API.Web.Dtos;
using API.Web.Result;

namespace API.Web.Service
{
    public interface IMealService
    {
        Result<MealDto> GetMeal(int id);
        Result<IEnumerable<MealDto>> GetMeals(bool isSaved);
        Result<IEnumerable<MealDto>> GetMeals(string mealName);
        Result<bool> MealNameValid(int id, string mealName);
        Result<MealDto> AddMeal(MealDto meal);
        Result<MealDto> EditMeal(MealDto meal);
        Result<MealDto> DeleteMeal(int id);
        Result<MealLogDto> AddMealLog(MealLogDto mealLog);
        Result<MealLogDto> DeleteMealLog(int mealLogId);
        Result<IEnumerable<MealLogDto>> GetMealLog();
        Result<IEnumerable<MealLogDto>> GetMealLog(DateTime date);
    }
}