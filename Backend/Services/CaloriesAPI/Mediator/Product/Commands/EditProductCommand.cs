using CaloriesAPI.Dtos;
using MediatR;
using System;

namespace CaloriesAPI.Mediator.Commands
{
    public class EditProductCommand : IRequest
    {
        public Guid UserId { get; set; }
        public bool IsUserAdmin { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
