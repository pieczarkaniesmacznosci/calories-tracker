using API.Dtos;
using MediatR;

namespace API.Mediator.Command
{
    public class CreateMealCommand : IRequest
    {
        public int UserId { get; set; }
        public MealDto Meal { get; set; }
    }
}
