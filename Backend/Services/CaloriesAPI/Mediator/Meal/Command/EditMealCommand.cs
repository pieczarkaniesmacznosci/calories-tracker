using CaloriesAPI.Dtos;
using MediatR;
using System;

namespace CaloriesAPI.Mediator.Command
{
    public class EditMealCommand : IRequest
    {
        public Guid UserId { get; set; }
        public int MealId { get; set; }
        public MealDto Meal { get; set; }
    }
}
