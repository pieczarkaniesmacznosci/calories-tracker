using CaloriesAPI.Dtos;
using MediatR;

namespace CaloriesAPI.Mediator.Command
{
    public class EditMealLogCommand : IRequest
    {
        public int UserId { get; set; }
        public int MealLogId { get; set; }
        public MealLogDto MealLog { get; set; }
    }
}
