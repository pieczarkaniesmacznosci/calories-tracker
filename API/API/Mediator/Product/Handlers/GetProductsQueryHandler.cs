using API.Dtos;
using API.Mediator.Queries;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IAsyncRepository<Product> _productAsyncRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(
            IAsyncRepository<Product> productAsyncRepository,
            IMapper mapper)
        {
            _productAsyncRepository = productAsyncRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<ProductDto>>(await _productAsyncRepository.FindAsync(x => (x.UserId == request.UserId || x.IsDefault == true) && x.IsAvailable));
            return result;
        }
    }
}
