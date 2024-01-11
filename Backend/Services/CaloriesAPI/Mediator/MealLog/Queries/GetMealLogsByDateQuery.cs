using CaloriesAPI.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace CaloriesAPI.Mediator.Query
{
    public class GetMealLogsByDateQuery : IRequest<IEnumerable<MealLogDto>>
    {
        public Guid UserId { get; set; }
        public DateTime MealLogDate { get; set; }
    }
}
