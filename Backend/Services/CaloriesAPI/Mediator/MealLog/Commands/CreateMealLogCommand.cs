using CaloriesAPI.Dtos;
using MediatR;

namespace CaloriesAPI.Mediator.Command
{
    public class CreateMealLogCommand : IRequest
    {
        public int UserId { get; set; }
        public MealLogDto MealLog { get; set; }
    }
}
