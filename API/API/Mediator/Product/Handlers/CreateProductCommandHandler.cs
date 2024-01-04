using API.Mediator.Commands;
using API.Validators;
using AutoMapper;
using Entities;
using MediatR;
using Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductValidator _productValidator;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Product> _productAsyncRepository;

        public CreateProductCommandHandler(
            IAsyncRepository<Product> productAsyncRepository,
            IMapper mapper,
            IProductValidator productValidator
            )
        {
            _productValidator = productValidator;
            _mapper = mapper;
            _productAsyncRepository = productAsyncRepository;
        }

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _productValidator.Validate(request.Product);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors.FirstOrDefault().ErrorMessage);
            }

            if (!(await IsProductNameValid(request.UserId, null, request.Product.Name)))
            {
                throw new ArgumentException($"Product name {request.Product.Name} is invalid!");
            }

            var productEntity = _mapper.Map<Product>(request.Product);

            productEntity.UserId = request.UserId;
            if (!request.IsUserAdmin)
            {
                productEntity.IsAvailable = true;
                productEntity.IsDefault = false;
                productEntity.DateAdded = DateTime.Now;
            }

            await _productAsyncRepository.AddAsync(productEntity);
        }

        private async Task<bool> IsProductNameValid(int userId, int? productId, string productName)
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
