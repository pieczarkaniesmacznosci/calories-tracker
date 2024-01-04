using CaloriesAPI.Dtos;
using MediatR;
using System.Collections.Generic;

namespace CaloriesAPI.Mediator.Query
{
    public class GetMealsByNameQuery : IRequest<IEnumerable<MealDto>>
    {
        public int UserId { get; set; }
        public string MealName { get; set; }
    }
}
