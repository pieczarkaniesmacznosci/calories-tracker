using API.Dtos;
using MediatR;
using System.Collections.Generic;

namespace API.Mediator.Query
{
    public class GetMealsByNameQuery : IRequest<IEnumerable<MealDto>>
    {
        public int UserId { get; set; }
        public string MealName { get; set; }
    }
}
