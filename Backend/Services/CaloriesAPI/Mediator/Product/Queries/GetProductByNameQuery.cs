using CaloriesAPI.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace CaloriesAPI.Mediator.Queries
{
    public class GetProductByNameQuery : IRequest<IEnumerable<ProductDto>>
    {
        public string ProductName { get; set; }
        public bool IsUserAdmin { get; set; }
        public Guid UserId { get; set; }
    }
}
