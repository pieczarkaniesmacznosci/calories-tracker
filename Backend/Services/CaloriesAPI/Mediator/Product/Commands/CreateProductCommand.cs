using CaloriesAPI.Dtos;
using MediatR;
using System;

namespace CaloriesAPI.Mediator.Commands
{
    public class CreateProductCommand : IRequest
    {
        public Guid UserId { get; set; }
        public bool IsUserAdmin { get; set; }
        public ProductDto Product { get; set; }
    }
}
