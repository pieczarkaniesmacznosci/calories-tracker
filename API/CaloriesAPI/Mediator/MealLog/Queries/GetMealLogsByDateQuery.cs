using CaloriesAPI.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace CaloriesAPI.Mediator.Query
{
    public class GetMealLogsByDateQuery : IRequest<IEnumerable<MealLogDto>>
    {
        public int UserId { get; set; }
        public DateTime MealLogDate { get; set; }
    }
}
