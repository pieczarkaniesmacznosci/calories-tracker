using API.Dtos;
using MediatR;

namespace API.Mediator.Query
{
    public class GetMealLogByIdQuery : IRequest<MealLogDto>
    {
        public int MealLogId { get; set; }
        public int UserId { get; set; }
    }
}
