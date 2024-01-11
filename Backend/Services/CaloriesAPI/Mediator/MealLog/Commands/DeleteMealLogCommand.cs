using MediatR;
using System;

namespace CaloriesAPI.Mediator.Command
{
    public class DeleteMealLogCommand : IRequest
    {
        public Guid UserId { get; set; }
        public int MealLogId { get; set; }
    }
}
