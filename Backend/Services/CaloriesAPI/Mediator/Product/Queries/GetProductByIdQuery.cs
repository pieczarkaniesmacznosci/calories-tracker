using CaloriesAPI.Dtos;
using MediatR;

namespace CaloriesAPI.Mediator.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public bool IsUserAdmin { get; set; }
    }
}
