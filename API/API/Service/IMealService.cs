using System;
using System.Collections.Generic;
using API.Dtos;
using API.Result;

namespace API.Service
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
        Result<MealLogDto> EditEatenMeal(int mealLogId, MealDto meal);
        Result<MealLogDto> AddMealLog(MealLogDto mealLog);
        Result<MealLogDto> DeleteMealLog(int mealLogId);
        Result<IEnumerable<MealLogDto>> GetMealLog();
        Result<MealLogDto> GetMealLog(int mealLogId);
        Result<IEnumerable<MealLogDto>> GetMealLog(DateTime date);
    }
}