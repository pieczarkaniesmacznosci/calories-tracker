using CaloriesAPI.Dtos;
using MediatR;
using System.Collections.Generic;

namespace CaloriesAPI.Mediator.Query
{
    public class GetMealLogsQuery : IRequest<IEnumerable<MealLogDto>>
    {
        public int UserId { get; set; }
    }
}
