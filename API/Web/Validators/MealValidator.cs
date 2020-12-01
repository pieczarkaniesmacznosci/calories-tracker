using System;
using System.Collections.Generic;
using System.Linq;
using API.Web.Dtos;
using API.Web.Entities;
using FluentValidation;

namespace API.Web.Validators
{
    public class MealValidator : AbstractValidator<MealDto>
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
}