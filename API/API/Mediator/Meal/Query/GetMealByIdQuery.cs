using API.Dtos;
using MediatR;

namespace API.Mediator.Query
{
    public class GetMealByIdQuery : IRequest<MealDto>
    {
        public int UserId { get; set; }
        public int MealId { get; set; }
    }
}
