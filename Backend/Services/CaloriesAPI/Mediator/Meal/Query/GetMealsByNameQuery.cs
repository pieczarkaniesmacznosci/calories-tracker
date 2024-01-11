using CaloriesAPI.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace CaloriesAPI.Mediator.Query
{
    public class GetMealsByNameQuery : IRequest<IEnumerable<MealDto>>
    {
        public Guid UserId { get; set; }
        public string MealName { get; set; }
    }
}
