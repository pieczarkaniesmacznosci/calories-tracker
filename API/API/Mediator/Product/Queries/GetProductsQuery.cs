using API.Dtos;
using MediatR;
using System.Collections.Generic;

namespace API.Mediator.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public bool IsUserAdmin { get; set; }
        public int UserId { get; set; }
    }
}
