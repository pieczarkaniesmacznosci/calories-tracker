using API.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserWeightDto>> GetUserWeightsAsync(int userId);
        Task<UserWeightDto> GetUserWeightAsync(int userId);
        Task<UserWeightDto> GetUserWeightAsync(int userId, DateTime date);
        Task AddUserWeightAsync(UserWeightDto userWeight);
        Task EditUserWeightAsync(UserWeightDto userWeight);
        Task DeleteUserWeightAsync(int userId, int userWeightId);
        Task<IEnumerable<UserNutritionDto>> GetUserNutritionsAsync(int userId);
        Task<UserNutritionDto> GetUserCurrentNutritionAsync(int userId);
        Task<UserNutritionDto> GetUserNutritionAsync(int userId, DateTime date);
        Task AddUserNutritionAsync(int userId, UserNutritionDto userNutrition);
        Task EditUserNutritionAsync(int userId, UserNutritionDto userNutrition);
        Task DeleteUserNutritionAsync(int userId, int userNutritionId);
    }
}
