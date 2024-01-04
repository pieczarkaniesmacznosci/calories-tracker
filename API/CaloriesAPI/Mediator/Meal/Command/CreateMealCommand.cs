using CaloriesAPI.Dtos;
using MediatR;

namespace CaloriesAPI.Mediator.Command
{
    public class CreateMealCommand : IRequest
    {
        public int UserId { get; set; }
        public MealDto Meal { get; set; }
    }
}
