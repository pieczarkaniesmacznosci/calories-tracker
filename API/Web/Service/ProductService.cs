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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Web.Extensions;

namespace API.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly ProductValidator _productValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private int _userId => GetCurrentUserId().Result;
        private bool _isUserAdmin => IsCurrentUserAdminRole().Result;

        private async Task<int> GetCurrentUserId()
        {
            var  loggedInUserName  = _httpContextAccessor.HttpContext.User.GetLoggedInUserName();
            var currentUserId = await _userManager.FindByNameAsync(loggedInUserName);

            return currentUserId.Id;
        }

        private async Task<bool> IsCurrentUserAdminRole()
        {
            var  loggedInUserName  = _httpContextAccessor.HttpContext.User.GetLoggedInUserName();
            var user = _userManager.FindByNameAsync(loggedInUserName).Result;
            var  loggedInUserRole  = await _userManager.IsInRoleAsync(user,"Admin");

            return loggedInUserRole;
        }

        public ProductService(
            ILogger<ProductService> logger, 
            IRepository<Product> productRepository, 
            IMapper mapper, 
            ProductValidator productValidator, 
            IHttpContextAccessor httpContextAccessor, 
            UserManager<User> userManager)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
            _productValidator = productValidator;
            _httpContextAccessor = httpContextAccessor;
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
                    .Find(x=> EF.Functions.Like(x.Name, $"%{productName}%"))
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
                var validationResult = _productValidator.Validate(product);
                if(!validationResult.IsValid)
                {
                    return new InvalidResult<ProductDto>(validationResult.Errors.FirstOrDefault().ErrorMessage);
                }
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
                var validationResult = _productValidator.Validate(product);
                if(!validationResult.IsValid)
                {
                    return new InvalidResult<ProductDto>(validationResult.Errors.FirstOrDefault().ErrorMessage);
                }
                            
                var productToEdit = _productRepository.Get(product.Id.Value);

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
                _logger.LogCritical($"Exception while editing product with name = {product.Name}",ex);
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

        public Result<bool> ProductNameValid(int id, string productName)
        {
            try
            {
                if(_productRepository.Find(x=> x.Name == productName && x.Id != id).FirstOrDefault() != null){
                    return new SuccessResult<bool>(false);
                }
                else{
                    return new SuccessResult<bool>(true);
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while checking if product with name = {productName} exists",ex);
                return new UnexpectedResult<bool>();
            }
        }
    }
}