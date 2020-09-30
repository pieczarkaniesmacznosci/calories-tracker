using System;
using System.Collections.Generic;
using API.Web.Dtos;
using API.Web.Result;

namespace API.Web.Service
{
    public interface IUserService
    {
        Result<IEnumerable<UserWeightDto>> GetUserWeights();
        Result<UserWeightDto> GetCurrentUserWeight();
        Result<UserWeightDto> GetUserWeight(DateTime date);
        Result<UserWeightDto> AddUserWeight(UserWeightDto userWeight);
        Result<UserWeightDto> EditUserWeight(UserWeightDto userWeight);
        Result<UserWeightDto> DeleteUserWeight(UserWeightDto userWeight);
        Result<IEnumerable<UserNutritionDto>> GetUserNutritions();
        Result<UserNutritionDto> GetCurrentUserNutrition();
        Result<UserNutritionDto> GetUserNutrition(DateTime date);
        Result<UserNutritionDto> AddUserNutrition(UserNutritionDto userNutrition);
        Result<UserNutritionDto> EditUserNutrition(UserNutritionDto userNutrition);
        Result<UserNutritionDto> DeleteUserNutrition(UserNutritionDto userNutrition);
    }
}