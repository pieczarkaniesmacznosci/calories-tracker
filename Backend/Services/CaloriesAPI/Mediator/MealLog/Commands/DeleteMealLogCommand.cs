using MediatR;

namespace CaloriesAPI.Mediator.Command
{
    public class DeleteMealLogCommand : IRequest
    {
        public int UserId { get; set; }
        public int MealLogId { get; set; }
    }
}
