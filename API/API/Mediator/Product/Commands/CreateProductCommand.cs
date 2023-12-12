using API.Dtos;
using MediatR;

namespace API.Mediator.Commands
{
    public class CreateProductCommand : IRequest
    {
        public int UserId { get; set; }
        public bool IsUserAdmin { get; set; }
        public ProductDto Product { get; set; }
    }
}
