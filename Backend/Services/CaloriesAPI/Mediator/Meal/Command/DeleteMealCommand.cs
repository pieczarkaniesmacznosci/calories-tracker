using MediatR;

namespace CaloriesAPI.Mediator.Command
{
    public class DeleteMealCommand : IRequest
    {
        public int UserId { get; set; }
        public int MealId { get; set; }
    }
}
