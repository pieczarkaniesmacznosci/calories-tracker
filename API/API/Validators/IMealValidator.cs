using API.Dtos;
using FluentValidation.Results;

namespace API.Validators
{

    public interface IMealValidator
    {
        ValidationResult Validate(MealDto product);
    }
}
