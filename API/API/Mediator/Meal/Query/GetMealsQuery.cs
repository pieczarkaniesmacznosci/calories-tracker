using API.Dtos;
using MediatR;
using System.Collections.Generic;

namespace API.Mediator.Query
{
    public class GetMealsQuery : IRequest<IEnumerable<MealDto>>
    {
        public int UserId { get; set; }
    }
}
