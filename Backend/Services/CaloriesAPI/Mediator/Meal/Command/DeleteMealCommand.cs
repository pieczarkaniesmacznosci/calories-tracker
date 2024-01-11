using MediatR;
using System;

namespace CaloriesAPI.Mediator.Command
{
    public class DeleteMealCommand : IRequest
    {
        public Guid UserId { get; set; }
        public int MealId { get; set; }
    }
}
