using MediatR;

namespace CaloriesAPI.Mediator.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public int UserId { get; set; }
        public bool IsUserAdmin { get; set; }
        public int ProductId { get; set; }
    }
}
