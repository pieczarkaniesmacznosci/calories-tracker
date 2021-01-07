using System;
using System.Collections.Generic;
using System.Linq;
using API.Web.Entities;
using API.Web.Dtos;
using API.Web.Repositories;
using API.Web.Result;
using AutoMapper;
using Microsoft.Extensions.Logging;
using API.Web.Validators;
using Microsoft.EntityFrameworkCore;
using API.Web.Identity;
using Web.Result.ErrorDefinitions;

namespace API.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IProductValidator _productValidator;
        private readonly IUserManager _userManager;
        private int _userId => _userManager.CurrentUserId;
        private bool _isUserAdmin => _userManager.IsCurrentUserAdmin;

        public ProductService(
            ILogger<ProductService> logger, 
            IRepository<Product> productRepository, 
            IMapper mapper, 
            IProductValidator productValidator,
            IUserManager userManager)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
            _productValidator = productValidator;
            _userManager = userManager;
        }

        public Result<IEnumerable<ProductDto>> GetProducts()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<ProductDto>>(_productRepository.Find(x=>x.UserId == _userId || x.IsDefault == true));
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
                Product product;

                if(!_isUserAdmin)
                {
                    product = _productRepository.Find(x=>x.UserId == _userId && x.Id == id).FirstOrDefault();
                }
                else
                {
                    product = _productRepository.Get(id);
                }

                if(product == null)
                {
                    _logger.LogInformation($"Product with id = {id} was not found!");
                    return new NotFoundResult<ProductDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError,new string[]{"Product",id.ToString()}));
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
                    .Find(x=> EF.Functions.Like(x.Name, $"%{productName}%") && x.UserId == _userId)
                    .ToList();

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
                var validationResult = _productValidator.Validate(product);
                if(!validationResult.IsValid)
                {
                    return new InvalidResult<ProductDto>(validationResult.Errors.FirstOrDefault().ErrorMessage);
                }

                if(!IsProductNameValid(_userId, product?.Id, product.Name))
                {
                    return new InvalidResult<ProductDto>($"Product name {product.Name} is invalid!");
                }
                     
                var productEntity = _mapper.Map<Product>(product);

                productEntity.UserId = _userId;
                
                if(!_isUserAdmin)
                {
                    productEntity.IsDefault = false;
                }
                
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
                var validationResult = _productValidator.Validate(product);
                if(!validationResult.IsValid)
                {
                    return new InvalidResult<ProductDto>(validationResult.Errors.FirstOrDefault().ErrorMessage);
                }

                Product productToEdit;

                if(!_isUserAdmin)
                {
                    productToEdit = _productRepository.Find(x=>x.UserId == _userId && x.Id == product.Id.Value).FirstOrDefault();
                    product.IsDefault = false;
                }
                else
                {
                    productToEdit = _productRepository.Get(product.Id.Value);
                }

                if(productToEdit == null)
                {
                    _logger.LogInformation($"Product with id = {product.Id} was not found!");
                    return new NotFoundResult<ProductDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError,new string[]{"Product",product.Id.ToString()}));
                }

                if(!IsProductNameValid(_userId, product?.Id, product.Name))
                {
                    return new InvalidResult<ProductDto>($"Product name {product.Name} is invalid!");
                }
                                
                var productEntity = _mapper.Map<Product>(product);
                var result = _productRepository.Update(productEntity);
                _productRepository.SaveChanges();

                return new SuccessResult<ProductDto>(_mapper.Map<ProductDto>(result));
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while editing product with name = {product.Name}",ex);
                return new UnexpectedResult<ProductDto>();
            }
        }

        public Result<ProductDto> DeleteProduct(int id)
        {
            try
            {
                Product productToDelete;
                if(!_isUserAdmin)
                {
                    productToDelete = _productRepository.Find(x=>x.UserId == _userId && x.Id == id).FirstOrDefault();
                }
                else
                {
                    productToDelete = _productRepository.Get(id);
                }

                if(productToDelete == null)
                {
                    _logger.LogInformation($"Product with id = {id} was not found!");
                    return new NotFoundResult<ProductDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError,new string[]{"Product",id.ToString()}));
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

        public Result<bool> ProductNameValid(int id, string productName)
        {
            try
            {
                var isNameValid = IsProductNameValid(_userId, id, productName);
                return new SuccessResult<bool>(isNameValid);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while checking if product with name = {productName} exists",ex);
                return new UnexpectedResult<bool>();
            }
        }

        private bool IsProductNameValid(int userId, int? productId, string productName)
        {
            if(_productRepository.Find(x=>x.Name == productName && (x.UserId == userId || x.IsDefault) && (productId.HasValue ? x.Id != productId : true)).FirstOrDefault() != null)
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