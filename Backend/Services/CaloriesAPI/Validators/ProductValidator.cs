using CaloriesAPI.Dtos;
using FluentValidation;

namespace CaloriesAPI.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>, IProductValidator
    {
        public ProductValidator()
        {
            RuleFor(x => x.Kcal)
                .InclusiveBetween(0, 999)
                .WithMessage("Calories for 100g of product must be in the (0-999) range.");
            RuleFor(x => x.Protein)
                .InclusiveBetween(0, 100)
                .WithMessage($"Product has to contain (0-100 g) of protein.");
            RuleFor(x => x.Carbohydrates)
                .InclusiveBetween(0, 100)
                .WithMessage($"Product has to contain (0-100 g) of carbohydrates.");
            RuleFor(x => x.Fat)
                .InclusiveBetween(0, 100)
                .WithMessage($"Product has to contain (0-100 g) of fat.");
            RuleFor(x => x)
                .Must(ContainsAtLeastOneMacronutrient)
                .WithMessage($"Product has to contain at least one macro-nutrient.");
        }

        private bool ContainsAtLeastOneMacronutrient(ProductDto product)
        {
            return (product.Protein + product.Carbohydrates + product.Fat) > 0;
        }
    }
}
