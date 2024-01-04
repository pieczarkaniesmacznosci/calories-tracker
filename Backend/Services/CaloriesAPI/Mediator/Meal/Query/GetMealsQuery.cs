using CaloriesAPI.Dtos;
using MediatR;
using System.Collections.Generic;

namespace CaloriesAPI.Mediator.Query
{
    public class GetMealsQuery : IRequest<IEnumerable<MealDto>>
    {
        public int UserId { get; set; }
    }
}
