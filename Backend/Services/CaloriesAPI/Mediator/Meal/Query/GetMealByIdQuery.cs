using CaloriesAPI.Dtos;
using MediatR;
using System;

namespace CaloriesAPI.Mediator.Query
{
    public class GetMealByIdQuery : IRequest<MealDto>
    {
        public Guid UserId { get; set; }
        public int MealId { get; set; }
    }
}
