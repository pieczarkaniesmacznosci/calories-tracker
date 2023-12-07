using API.Dtos;
using API.Mediator.Queries;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly ILogger<GetProductByIdQueryHandler> _logger;
        private readonly GenericAsyncRepository<Product> _productAsyncRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(ILogger<GetProductByIdQueryHandler> logger, GenericAsyncRepository<Product> productAsyncRepository, IMapper mapper)
        {
            _logger = logger;
            _productAsyncRepository = productAsyncRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Product product;
                if (!request.IsUserAdmin)
                {
                    product = (await _productAsyncRepository.FindAsync(x => (x.UserId == request.UserId || x.IsDefault) && x.Id == request.Id && x.IsAvailable)).SingleOrDefault();
                }
                else
                {
                    product = await _productAsyncRepository.GetByIdAsync(request.Id);
                }

                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while getting product with id= {id}", request.Id);
                throw;
            }
        }
    }
}
