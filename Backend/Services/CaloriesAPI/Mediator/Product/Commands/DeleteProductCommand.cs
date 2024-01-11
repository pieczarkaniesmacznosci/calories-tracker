using MediatR;
using System;

namespace CaloriesAPI.Mediator.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public Guid UserId { get; set; }
        public bool IsUserAdmin { get; set; }
        public int ProductId { get; set; }
    }
}
