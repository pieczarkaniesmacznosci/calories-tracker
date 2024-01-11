using CaloriesAPI.Dtos;
using MediatR;
using System;

namespace CaloriesAPI.Mediator.Query
{
    public class GetMealLogByIdQuery : IRequest<MealLogDto>
    {
        public int MealLogId { get; set; }
        public Guid UserId { get; set; }
    }
}
