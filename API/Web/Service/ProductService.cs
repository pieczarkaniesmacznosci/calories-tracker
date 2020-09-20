using System;
using System.Collections.Generic;
using System.Linq;
using API.Web.Entities;
using API.Web.Dtos;
using API.Web.Repositories;
using API.Web.Result;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace API.Web.Service
{
    public class ProductService : IProductService
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
                var product = _productRepository.Get(id);

                if(product == null)
                {
                    _logger.LogInformation($"Product with id = {id} was not found!");
                    return new NotFoundResult<ProductDto>();
                }

                var productDto = _mapper.Map<ProductDto>(product);
                return new SuccessResult<ProductDto>(productDto);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting product with id = {id}",ex);
                return new UnexpectedResult<ProductDto>();
            }
        }

        public Result<IEnumerable<ProductDto>> GetProducts(string productName)
        {
            try
            { 
                var products = _productRepository
                    .All()
                    .Where(x=>x.Name.StartsWith(productName,System.StringComparison.InvariantCulture))
                    .ToList();

                if(products.Count == 0)
                {
                    _logger.LogInformation($"No products starting with {productName} were found!");
                    return new NotFoundResult<IEnumerable<ProductDto>>();
                }

                var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
                return new SuccessResult<IEnumerable<ProductDto>>(productsDto);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting products",ex);
                return new UnexpectedResult<IEnumerable<ProductDto>>();
            }
        }

        public Result<ProductDto> AddProduct(ProductDto product)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(product);
                var result = _productRepository.Add(productEntity);
                _productRepository.SaveChanges();
                return new SuccessResult<ProductDto>(_mapper.Map<ProductDto>(result));
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while adding product with name = {product.Name}",ex);
                return new UnexpectedResult<ProductDto>();
            }
        }

        public Result<ProductDto> EditProduct(ProductDto product)
        {
            try
            {                
                var productToEdit = _productRepository.Get(product.Id);

                if(productToEdit == null)
                {
                    _logger.LogInformation($"Product with id = {product.Id} was not found!");
                    return new NotFoundResult<ProductDto>();
                }

                var productEntity = _mapper.Map<Product>(product);
                var result = _productRepository.Update(productEntity);
                _productRepository.SaveChanges();

                return new SuccessResult<ProductDto>(_mapper.Map<ProductDto>(result));
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while adding editing with name = {product.Name}",ex);
                return new UnexpectedResult<ProductDto>();
            }
        }

        public Result<ProductDto> DeleteProduct(int id)
        {
            try
            {
                var productToDelete = _productRepository.Get(id);

                if(productToDelete == null)
                {
                    _logger.LogInformation($"Product with id = {id} was not found!");
                    return new NotFoundResult<ProductDto>();
                }

                var result = _productRepository.Delete(productToDelete);
                _productRepository.SaveChanges();
                return new SuccessResult<ProductDto>(_mapper.Map<ProductDto>(result));
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while deleting product with id = {id}",ex);
                return new UnexpectedResult<ProductDto>();
            }
        }
    }
}