using CaloriesAPI.Dtos;
using MediatR;

namespace CaloriesAPI.Mediator.Query
{
    public class GetMealByIdQuery : IRequest<MealDto>
    {
        public int UserId { get; set; }
        public int MealId { get; set; }
    }
}
