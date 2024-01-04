using CaloriesAPI.Dtos;
using FluentValidation.Results;

namespace CaloriesAPI.Validators
{

    public interface IMealValidator
    {
        ValidationResult Validate(MealDto product);
    }
}
