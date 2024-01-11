using CaloriesAPI.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace CaloriesAPI.Mediator.Query
{
    public class GetMealLogsQuery : IRequest<IEnumerable<MealLogDto>>
    {
        public Guid UserId { get; set; }
    }
}
