using CaloriesAPI.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace CaloriesAPI.Mediator.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public bool IsUserAdmin { get; set; }
        public Guid UserId { get; set; }
    }
}
