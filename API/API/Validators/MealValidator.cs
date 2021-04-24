using System.Collections.Generic;
using System.Linq;
using API.Dtos;
using FluentValidation;
using FluentValidation.Results;

namespace API.Validators
{
    public class MealValidator : AbstractValidator<MealDto>, IMealValidator
    {
        public MealValidator()
        {
            RuleFor(x=>x.MealProducts)
            .Cascade(CascadeMode.Stop)
                .Must(ContainAtLeastOneProduct)
                .WithMessage("Meal has to contain at least one product !")
                .Must(EveryProductHasWeight)
                .WithMessage("Meal product cannot have weight of 0g !");
        }

        private bool EveryProductHasWeight(IEnumerable<MealProductDto> mealProducts)
        {
            return !mealProducts.Any(x=>x.Weight == 0);
        }

        private bool ContainAtLeastOneProduct(IEnumerable<MealProductDto> mealProducts)
        {
            return mealProducts.Count() > 0;
        }
    }

    public interface IMealValidator{
        ValidationResult Validate(MealDto product);
    }
}