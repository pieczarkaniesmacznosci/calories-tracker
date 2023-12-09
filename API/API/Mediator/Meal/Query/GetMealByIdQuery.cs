using API.Dtos;
using MediatR;

namespace API.Mediator.Query
{
    public class GetMealByIdQuery : IRequest<MealDto>
    {
        public int MealId { get; set; }
        public int UserId { get; set; }
    }
}
