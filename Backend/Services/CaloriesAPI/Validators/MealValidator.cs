using CaloriesAPI.Dtos;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace CaloriesAPI.Validators
{
    public class MealValidator : AbstractValidator<MealDto>, IMealValidator
    {
        public MealValidator()
        {
            RuleFor(x => x.MealProducts)
            .Cascade(CascadeMode.Stop)
                .Must(ContainAtLeastOneProduct)
                .WithMessage("Meal has to contain at least one product.")
                .Must(EveryProductHasWeight)
                .WithMessage("Meal product has to weight more than 0g.");
        }

        private bool EveryProductHasWeight(IEnumerable<MealProductDto> mealProducts)
        {
            return !mealProducts.Any(x => x.Weight == 0);
        }

        private bool ContainAtLeastOneProduct(IEnumerable<MealProductDto> mealProducts)
        {
            return mealProducts.Any();
        }
    }
}
