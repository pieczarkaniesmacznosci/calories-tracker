using CaloriesAPI.Dtos;
using MediatR;
using System;

namespace CaloriesAPI.Mediator.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public bool IsUserAdmin { get; set; }
    }
}
