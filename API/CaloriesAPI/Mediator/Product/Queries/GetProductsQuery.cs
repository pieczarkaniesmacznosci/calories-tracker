using CaloriesAPI.Dtos;
using MediatR;
using System.Collections.Generic;

namespace CaloriesAPI.Mediator.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public bool IsUserAdmin { get; set; }
        public int UserId { get; set; }
    }
}
