using API.Dtos;
using MediatR;
using System.Collections.Generic;

namespace API.Mediator.Query
{
    public class GetMealLogsQuery : IRequest<IEnumerable<MealLogDto>>
    {
        public int UserId { get; set; }
    }
}
