using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using API.Dtos;
using API.Entities;
using API.Identity;
using API.Repositories;
using API.Result;
using API.Service;
using API.Validators;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class ProductServiceTests
    {
        private Mock<ILogger<ProductService>> _logger;
        private Mock<IRepository<Product>> _productRepository;
        private Mock<IMapper> _mapper;
        private Mock<IProductValidator> _productValidator;
        private Mock<IUserManager> _userManager;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<ProductService>>();
            _productRepository = new Mock<IRepository<Product>>();
            _mapper = new Mock<IMapper>();
            _productValidator = new Mock<IProductValidator>();
            _userManager = new Mock<IUserManager>();
        }

        [Test]
        public void GetProducts_WhenValid_ReturnsSuccessResultWithCollectionOfUserAndDefaultProducts()
        {
            var userId = 1;
            var products = new List<Product>{
                new Product{
                    UserId = userId,
                    IsDefault = false
                },
                new Product{
                    UserId = userId,
                    IsDefault = false
                },
                new Product{
                    UserId = userId,
                    IsDefault = true
                }
            };

            var productsDto = new List<ProductDto>{
                new ProductDto{
                    IsDefault = false,
                },
                new ProductDto{
                    IsDefault = false,
                },
                new ProductDto{
                    IsDefault = true
                }
            };
            _productRepository.Setup(x=>x.Find(It.IsAny<Expression<Func<Product,bool>>>())).Returns(products);

            _mapper.Setup(x=>x.Map<IEnumerable<ProductDto>>(products)).Returns(productsDto);
            
            var productService = new ProductService(
                _logger.Object,
                _productRepository.Object,
                _mapper.Object,
                _productValidator.Object,
                _userManager.Object);

            var result = productService.GetProducts();

            Assert.IsEmpty(result.Errors);
            Assert.IsNotEmpty(result.Data);
            Assert.AreEqual(productsDto.Count, ((List<ProductDto>)result.Data).Count);
            Assert.AreEqual(ResultType.Ok, result.ResultType);
        }
        
        [Test]
        public void GetProduct_WhenValidUserHasUserRole_ReturnsSuccessResultWithUserProduct()
        {
            var productId = It.IsAny<int>();
            var productName = "Test1";
            var userId = 1;
            var isUserAdmin = false;
            
            var product = new List<Product>{
                new Product{
                    UserId = userId,
                    Name = productName,
                    IsDefault = false
                    }
                };

            var productDto = 
                new ProductDto{
                    Name = productName,
                    IsDefault = false
                };

            _userManager.Setup(x=>x.CurrentUserId).Returns(userId);
            _userManager.Setup(x=>x.IsCurrentUserAdmin).Returns(isUserAdmin);

            _productRepository.Setup(x=>x.Find(It.IsAny<Expression<Func<Product,bool>>>())).Returns(product);

            _mapper.Setup(x=>x.Map<ProductDto>(product.FirstOrDefault())).Returns(productDto);
            
            var productService = new ProductService(
                _logger.Object,
                _productRepository.Object,
                _mapper.Object,
                _productValidator.Object,
                _userManager.Object);

            var result = productService.GetProduct(productId);

            Assert.IsEmpty(result.Errors);
            Assert.AreEqual(productName, ((ProductDto)result.Data).Name);
            Assert.AreEqual(ResultType.Ok, result.ResultType);
        }

        [Test]
        public void GetProduct_WhenValidUserHasAdminRole_ReturnsSuccessResultWithAnyUserProduct()
        {
            var productId = It.IsAny<int>();
            var productName = "Test1";
            var userId = 1;
            var isUserAdmin = true;
            
            var product = new Product{
                    UserId = It.Is<int>(x=>x != userId),
                    Name = productName,
                    IsDefault = false
                };

            var productDto = 
                new ProductDto{
                    Name = productName,
                    IsDefault = false
                };

            _userManager.Setup(x=>x.CurrentUserId).Returns(userId);
            _userManager.Setup(x=>x.IsCurrentUserAdmin).Returns(isUserAdmin);

            _productRepository.Setup(x=>x.Get(It.IsAny<int>())).Returns(product);

            _mapper.Setup(x=>x.Map<ProductDto>(product)).Returns(productDto);
            
            var productService = new ProductService(
                _logger.Object,
                _productRepository.Object,
                _mapper.Object,
                _productValidator.Object,
                _userManager.Object);

            var result = productService.GetProduct(productId);

            Assert.IsEmpty(result.Errors);
            Assert.AreEqual(productName, ((ProductDto)result.Data).Name);
            Assert.AreEqual(ResultType.Ok, result.ResultType);
        }

        [Test]
        public void GetProduct_WhenInvalidIdUserHasAdminRole_ReturnsNotFoundResult()
        {
            var productId = It.IsAny<int>();
            var userId = 1;
            var isUserAdmin = true;
            
            _userManager.Setup(x=>x.CurrentUserId).Returns(userId);
            _userManager.Setup(x=>x.IsCurrentUserAdmin).Returns(isUserAdmin);

            _productRepository.Setup(x=>x.Get(It.IsAny<int>())).Returns((Product)null);

            
            var productService = new ProductService(
                _logger.Object,
                _productRepository.Object,
                _mapper.Object,
                _productValidator.Object,
                _userManager.Object);

            var result = productService.GetProduct(productId);

            Assert.IsEmpty(result.Errors);
            Assert.AreEqual(ResultType.NotFound, result.ResultType);
        }

        [Test]
        public void GetProduct_WhenValidUserHasUserRole_ReturnsNotFoundResult()
        {
            var productId = It.IsAny<int>();
            var userId = 1;
            var isUserAdmin = false;
            
            var product = new List<Product>();

            _userManager.Setup(x=>x.CurrentUserId).Returns(userId);
            _userManager.Setup(x=>x.IsCurrentUserAdmin).Returns(isUserAdmin);

            _productRepository.Setup(x=>x.Find(It.IsAny<Expression<Func<Product,bool>>>())).Returns(new List<Product>());

            var productService = new ProductService(
                _logger.Object,
                _productRepository.Object,
                _mapper.Object,
                _productValidator.Object,
                _userManager.Object);

            var result = productService.GetProduct(productId);

            Assert.IsEmpty(result.Errors);
            Assert.AreEqual(ResultType.NotFound, result.ResultType);
        }
    }
}