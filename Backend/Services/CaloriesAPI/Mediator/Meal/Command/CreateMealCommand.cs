using CaloriesAPI.Dtos;
using MediatR;
using System;

namespace CaloriesAPI.Mediator.Command
{
    public class CreateMealCommand : IRequest
    {
        public Guid UserId { get; set; }
        public MealDto Meal { get; set; }
    }
}
