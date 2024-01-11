using CaloriesAPI.Dtos;
using MediatR;
using System;

namespace CaloriesAPI.Mediator.Command
{
    public class CreateMealLogCommand : IRequest
    {
        public Guid UserId { get; set; }
        public MealLogDto MealLog { get; set; }
    }
}
