﻿using API.Dtos;
using MediatR;

namespace API.Mediator.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public bool IsUserAdmin { get; set; }
    }
}
