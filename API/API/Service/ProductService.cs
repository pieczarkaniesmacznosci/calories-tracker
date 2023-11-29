using API.Dtos;
using API.Identity;
using API.Result;
using API.Result.ErrorDefinitions;
using API.Validators;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Service
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
                var result = _mapper.Map<IEnumerable<ProductDto>>(_productRepository.Find(x => ((x.UserId == _userId || x.IsDefault == true) && x.IsAvailable)));
                return new SuccessResult<IEnumerable<ProductDto>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while getting products");
                return new UnexpectedResult<IEnumerable<ProductDto>>();
            }
        }

        public Result<ProductDto> GetProduct(int id)
        {
            try
            {
                Expression<Func<Product, bool>> searchFunction;
                if (!_isUserAdmin)
                {
                    searchFunction = x => (x.UserId == _userId || x.IsDefault) && x.Id == id && x.IsAvailable;
                }
                else
                {
                    searchFunction = x => x.Id == id;
                }

                var product = _productRepository.Find(searchFunction).FirstOrDefault();

                if (product == null)
                {
                    _logger.LogInformation("Product with id= {id} was not found!", id);
                    return new NotFoundResult<ProductDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Product", id.ToString() }));
                }

                var productDto = _mapper.Map<ProductDto>(product);
                return new SuccessResult<ProductDto>(productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while getting product with id= {id}", id);
                return new UnexpectedResult<ProductDto>();
            }
        }

        public Result<IEnumerable<ProductDto>> GetProducts(string productName)
        {
            try
            {
                Expression<Func<Product, bool>> searchFunction;
                if (!_isUserAdmin)
                {
                    searchFunction = x => EF.Functions.Like(x.Name, $"%{productName}%") && (x.UserId == _userId || x.IsDefault) && x.IsAvailable;
                }
                else
                {
                    searchFunction = x => EF.Functions.Like(x.Name, $"%{productName}%");
                }

                var products = _productRepository
                    .Find(searchFunction)
                    .ToList();

                var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
                return new SuccessResult<IEnumerable<ProductDto>>(productsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while getting products");
                return new UnexpectedResult<IEnumerable<ProductDto>>();
            }
        }

        public Result<ProductDto> AddProduct(ProductDto product)
        {
            try
            {
                var validationResult = _productValidator.Validate(product);
                if (!validationResult.IsValid)
                {
                    return new InvalidResult<ProductDto>(validationResult.Errors.FirstOrDefault().ErrorMessage);
                }

                if (!IsProductNameValid(_userId, product?.Id, product.Name))
                {
                    return new InvalidResult<ProductDto>($"Product name {product.Name} is invalid!");
                }

                var productEntity = _mapper.Map<Product>(product);

                productEntity.UserId = _userId;
                if (!_isUserAdmin)
                {
                    productEntity.IsAvailable = true;
                    productEntity.IsDefault = false;
                    productEntity.DateAdded = DateTime.Now;
                }

                var result = _productRepository.Add(productEntity);
                _productRepository.SaveChanges();
                return new SuccessResult<ProductDto>(_mapper.Map<ProductDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while adding product with name= {productName}", ex);
                return new UnexpectedResult<ProductDto>();
            }
        }

        public Result<ProductDto> EditProduct(int id, ProductDto product)
        {
            try
            {
                var validationResult = _productValidator.Validate(product);
                if (!validationResult.IsValid)
                {
                    return new InvalidResult<ProductDto>(validationResult.Errors.FirstOrDefault().ErrorMessage);
                }

                Product productToEdit;

                if (!_isUserAdmin)
                {
                    productToEdit = _productRepository.Find(x => x.UserId == _userId && x.Id == id && x.IsAvailable).FirstOrDefault();
                    if (product.DateAdded == null)
                    {
                        product.DateAdded = DateTime.Now;
                    }
                    product.IsAvailable = true;
                    product.IsDefault = false;
                    product.Id = null;
                }
                else
                {
                    productToEdit = _productRepository.Get(id);
                }

                if (productToEdit == null)
                {
                    _logger.LogInformation("Product with id= {id} was not found!", id);
                    return new NotFoundResult<ProductDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Product", id.ToString() }));
                }

                if (!IsProductNameValid(_userId, id, product.Name))
                {
                    return new InvalidResult<ProductDto>($"Product name {product.Name} is invalid!");
                }

                var productEntity = _mapper.Map<Product>(product);
                productEntity.UserId = _userId;
                productToEdit.IsAvailable = false;
                var editResult = _productRepository.Update(productToEdit);
                var addResult = _productRepository.Add(productEntity);
                _productRepository.SaveChanges();

                return new SuccessResult<ProductDto>(_mapper.Map<ProductDto>(addResult));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while editing product with name= {productName}", product.Name);
                return new UnexpectedResult<ProductDto>();
            }
        }

        public Result<ProductDto> DeleteProduct(int id)
        {
            try
            {
                Expression<Func<Product, bool>> searchFunction;
                if (!_isUserAdmin)
                {
                    searchFunction = x => x.UserId == _userId && x.Id == id && x.IsAvailable;
                }
                else
                {
                    searchFunction = x => x.Id == id;
                }

                var productToDelete = _productRepository.Find(searchFunction).FirstOrDefault();

                if (productToDelete == null)
                {
                    _logger.LogInformation("Product with id= {id} was not found!", id);
                    return new NotFoundResult<ProductDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Product", id.ToString() }));
                }

                Product result;

                if (!_isUserAdmin)
                {
                    productToDelete.IsAvailable = false;
                    result = _productRepository.Update(productToDelete);
                }
                else
                {
                    productToDelete = _productRepository.Get(id);
                    result = _productRepository.Delete(productToDelete);
                }

                _productRepository.SaveChanges();
                return new SuccessResult<ProductDto>(_mapper.Map<ProductDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while deleting product with id= {id}", id);
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
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Exception while checking if product with name= {productName} exists", productName);
                return new UnexpectedResult<bool>();
            }
        }

        private bool IsProductNameValid(int userId, int? productId, string productName)
        {
            if (_productRepository.Find(x => x.Name == productName && x.IsAvailable && (x.UserId == userId || x.IsDefault) && (productId.HasValue ? x.Id != productId : true)).FirstOrDefault() != null)
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
