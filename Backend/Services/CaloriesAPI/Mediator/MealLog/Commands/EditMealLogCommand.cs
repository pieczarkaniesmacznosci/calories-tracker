using CaloriesAPI.Dtos;
using MediatR;
using System;

namespace CaloriesAPI.Mediator.Command
{
    public class EditMealLogCommand : IRequest
    {
        public Guid UserId { get; set; }
        public int MealLogId { get; set; }
        public MealLogDto MealLog { get; set; }
    }
}
