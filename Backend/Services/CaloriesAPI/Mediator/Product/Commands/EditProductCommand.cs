using CaloriesAPI.Dtos;
using MediatR;

namespace CaloriesAPI.Mediator.Commands
{
    public class EditProductCommand : IRequest
    {
        public int UserId { get; set; }
        public bool IsUserAdmin { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
