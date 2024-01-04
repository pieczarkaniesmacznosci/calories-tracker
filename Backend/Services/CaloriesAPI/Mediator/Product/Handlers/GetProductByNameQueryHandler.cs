using AutoMapper;
using CaloriesAPI.Dtos;
using CaloriesAPI.Mediator.Queries;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesAPI.Mediator.Handlers
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IEnumerable<ProductDto>>
    {
        private readonly IAsyncRepository<Product> _productAsyncRepository;
        private readonly IMapper _mapper;

        public GetProductByNameQueryHandler(
            IAsyncRepository<Product> productAsyncRepository,
            IMapper mapper)
        {
            _productAsyncRepository = productAsyncRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products;
            if (!request.IsUserAdmin)
            {
                products = (await _productAsyncRepository.FindAsync(x => EF.Functions.Like(x.Name, $"%{request.ProductName}%") && (x.UserId == request.UserId || x.IsDefault) && x.IsAvailable));
            }
            else
            {
                products = await _productAsyncRepository.FindAsync(x => EF.Functions.Like(x.Name, $"%{request.ProductName}%"));
            }

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
