using CaloriesAPI.Mediator.Commands;
using Entities;
using MediatR;
using Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesAPI.Mediator.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IAsyncRepository<Product> _productAsyncRepository;

        public DeleteProductCommandHandler(IAsyncRepository<Product> productAsyncRepository)
        {
            _productAsyncRepository = productAsyncRepository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product productToDelete;
            if (!request.IsUserAdmin)
            {
                productToDelete = (await _productAsyncRepository.FindAsync(x => (x.UserId == request.UserId || x.IsDefault) && x.Id == request.ProductId && x.IsAvailable)).SingleOrDefault();
            }
            else
            {
                productToDelete = await _productAsyncRepository.GetByIdAsync(request.ProductId);
            }

            if (productToDelete == null)
            {
                throw new KeyNotFoundException($"Product with id= {request.ProductId} was not found!");
            }

            if (!request.IsUserAdmin)
            {
                productToDelete.IsAvailable = false;
                await _productAsyncRepository.UpdateAsync(productToDelete);
            }
            else
            {
                await _productAsyncRepository.DeleteAsync(productToDelete);
            }
        }
    }
}
