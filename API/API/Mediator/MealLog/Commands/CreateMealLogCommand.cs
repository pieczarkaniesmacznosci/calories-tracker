using API.Dtos;
using MediatR;

namespace API.Mediator.Command
{
    public class CreateMealLogCommand : IRequest
    {
        public int UserId { get; set; }
        public MealLogDto MealLog { get; set; }
    }
}
