using CaloriesAPI.Dtos;
using MediatR;

namespace CaloriesAPI.Mediator.Query
{
    public class GetMealLogByIdQuery : IRequest<MealLogDto>
    {
        public int MealLogId { get; set; }
        public int UserId { get; set; }
    }
}
