using System;
using API.Web.Dtos;
using FluentValidation;

namespace API.Web.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x=>x.Kcal)
                .InclusiveBetween(0,999)
                .WithMessage("Calories for 100g of product must be in the (0-999) range !");
            RuleFor(x=>x.Protein)
                .InclusiveBetween(0,100)
                .WithMessage($"Product contains (0-100 g) of protein!");
            RuleFor(x=>x.Carbohydrates)
                .InclusiveBetween(0,100)
                .WithMessage($"Product contains (0-100 g) of carbohydrates!");
            RuleFor(x=>x.Fat)
                .InclusiveBetween(0,100)
                .WithMessage($"Product contains (0-100 g) of fat!");
            RuleFor(x=>x)
                .Must(ContainAtLeastOneMacronutrient)
                .WithMessage($"Product has to contain at least one macro-nutrient!");
        }

        private bool ContainAtLeastOneMacronutrient(ProductDto product)
        {
            return (product.Protein + product.Carbohydrates + product.Fat) > 0;
        }
    }
}