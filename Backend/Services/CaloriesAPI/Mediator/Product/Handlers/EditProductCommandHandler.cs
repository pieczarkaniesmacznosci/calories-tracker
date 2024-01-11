using AutoMapper;
using CaloriesAPI.Mediator.Commands;
using CaloriesAPI.Validators;
using Entities;
using MediatR;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesAPI.Mediator.Handlers
{
    public class EditProductCommandHandler : IRequestHandler<EditProductCommand>
    {
        private readonly IAsyncRepository<Product> _productAsyncRepository;
        private readonly IProductValidator _productValidator;
        private readonly IMapper _mapper;

        public EditProductCommandHandler(
            IAsyncRepository<Product> productAsyncRepository,
            IProductValidator productValidator,
            IMapper mapper)
        {
            _productAsyncRepository = productAsyncRepository;
            _productValidator = productValidator;
            _mapper = mapper;
        }

        public async Task Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _productValidator.Validate(request.Product);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors.FirstOrDefault().ErrorMessage);
            }
            Product productToEdit;

            if (!request.IsUserAdmin)
            {
                productToEdit = (await _productAsyncRepository.FindAsync(x => x.UserId == request.UserId && x.Id == request.ProductId && x.IsAvailable)).FirstOrDefault();
                if (request.Product.DateAdded == null)
                {
                    request.Product.DateAdded = DateTime.Now;
                }
                request.Product.IsAvailable = true;
                request.Product.IsDefault = false;
                request.Product.Id = null;
            }
            else
            {
                productToEdit = await _productAsyncRepository.GetByIdAsync(request.ProductId);
            }

            if (productToEdit == null)
            {
                throw new KeyNotFoundException($"Product with id= {request.ProductId} was not found!");
            }

            if (!(await IsProductNameValid(request.UserId, request.ProductId, request.Product.Name)))
            {
                throw new ArgumentException($"Product name {request.Product.Name} is invalid!");
            }

            var productEntity = _mapper.Map<Product>(request.Product);
            productEntity.UserId = request.UserId;
            productToEdit.IsAvailable = false;
            await _productAsyncRepository.UpdateAsync(productToEdit);
            await _productAsyncRepository.AddAsync(productEntity);
        }

        private async Task<bool> IsProductNameValid(Guid userId, int? productId, string productName)
        {
            if ((await _productAsyncRepository.FindAsync(x => x.Name == productName && x.IsAvailable && (x.UserId == userId || x.IsDefault) && (productId.HasValue ? x.Id != productId : true))).FirstOrDefault() != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
