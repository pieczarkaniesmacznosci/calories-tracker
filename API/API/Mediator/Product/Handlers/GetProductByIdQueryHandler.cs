using API.Dtos;
using API.Mediator.Queries;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IAsyncRepository<Product> _productAsyncRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(
            IAsyncRepository<Product> productAsyncRepository,
            IMapper mapper)
        {
            _productAsyncRepository = productAsyncRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product product;
            if (!request.IsUserAdmin)
            {
                product = (await _productAsyncRepository.FindAsync(x => (x.UserId == request.UserId || x.IsDefault) && x.Id == request.ProductId && x.IsAvailable)).SingleOrDefault();
            }
            else
            {
                product = await _productAsyncRepository.GetByIdAsync(request.ProductId);
            }
            if (product == null)
            {
                throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Product", request.ProductId.ToString() }));
            }

            return _mapper.Map<ProductDto>(product);
        }
    }
}
