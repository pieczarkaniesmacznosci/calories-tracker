using System;
using System.Collections.Generic;
using System.Linq;
using API.Web.Entities;
using API.Web.Models;
using API.Web.Repositories;
using API.Web.Result;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace API.Web.Service
{
    public class ProductService : IService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(ILogger<ProductService> logger, IRepository<Product> productRepository, IMapper mapper)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Result<IEnumerable<ProductDto>> GetProducts()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<ProductDto>>(_productRepository.All());
                return new SuccessResult<IEnumerable<ProductDto>>(result);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting products",ex);
                return new UnexpectedResult<IEnumerable<ProductDto>>();
            }
        }

        public Result<ProductDto> GetProduct(int id)
        {
            try
            {                
                var productToReturn = _productRepository.Get(id);

                if(productToReturn == null)
                {
                    _logger.LogInformation($"Product with id = {id} was not found!");
                    return new NotFoundResult<ProductDto>();
                }

                var result = _mapper.Map<ProductDto>(_productRepository.Get(id));
                return new SuccessResult<ProductDto>(result);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting product with id = {id}",ex);
                return new UnexpectedResult<ProductDto>();
            }
        }

        public IEnumerable<Product> GetProducts(string productName)
        {
            return _productRepository.All().Where(x=>x.Name.StartsWith(productName,System.StringComparison.InvariantCulture));
        }

        public Product AddProduct(ProductDto product)
        {
            var entity = _mapper.Map<Product>(product);
            var result = _productRepository.Add(entity);
            _productRepository.SaveChanges();
            return result;
        }

        public Product EditProduct(ProductDto product)
        {
            var entity = _mapper.Map<Product>(product);
            var result = _productRepository.Update(entity);
            _productRepository.SaveChanges();
            return result;
        }

        public Product DeleteProduct(int id)
        {
            var productToDelete = _productRepository.Find(x=>x.Id == id).SingleOrDefault();
            var result = _productRepository.Delete(productToDelete);
            _productRepository.SaveChanges();
            return result;
        }
    }
}