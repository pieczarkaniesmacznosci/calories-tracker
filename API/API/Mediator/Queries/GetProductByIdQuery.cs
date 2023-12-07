using API.Dtos;
using MediatR;

namespace API.Mediator.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
        public bool IsUserAdmin { get; set; }
        public int UserId { get; set; }
    }
}
