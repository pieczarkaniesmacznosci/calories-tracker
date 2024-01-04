using CaloriesAPI.Dtos;
using FluentValidation.Results;

namespace CaloriesAPI.Validators
{
    public interface IProductValidator
    {
        ValidationResult Validate(ProductDto product);
    }
}
