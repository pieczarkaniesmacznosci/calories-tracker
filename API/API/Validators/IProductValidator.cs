using API.Dtos;
using FluentValidation.Results;

namespace API.Validators
{
    public interface IProductValidator
    {
        ValidationResult Validate(ProductDto product);
    }
}
