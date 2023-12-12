using API.Dtos;
using API.Mediator.Handlers;
using API.Mediator.Queries;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    public class ProductHandlersTests
    {
        private Mock<IAsyncRepository<Product>> _productRepository;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void Setup()
        {
            _productRepository = new Mock<IAsyncRepository<Product>>();
            _mapper = new Mock<IMapper>();
        }

        [Test]
        public async Task GetProductsQueryHandler_WhenValid_ReturnsSuccessResultWithCollectionOfUserAndDefaultProducts()
        {
            int userId = 1;
            List<Product> products = new(){
                new(){
                    UserId = userId,
                    IsDefault = false
                },
                new(){
                    UserId = userId,
                    IsDefault = false
                },
                new(){
                    UserId = userId,
                    IsDefault = true
                }
            };

            List<ProductDto> productsDto = new(){
                new(){
                    IsDefault = false,
                },
                new(){
                    IsDefault = false,
                },
                new(){
                    IsDefault = true
                }
            };

            _productRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())).Returns(Task.FromResult(products.AsEnumerable()));

            _mapper.Setup(x => x.Map<IEnumerable<ProductDto>>(products)).Returns(productsDto);

            GetProductsQueryHandler getProductsQueryHandler = new(
                _productRepository.Object,
                _mapper.Object);

            var result = await getProductsQueryHandler.Handle(new GetProductsQuery(), CancellationToken.None);

            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public async Task GetProductByIdQueryHandler_WhenValidUserHasUserRole_ReturnsSuccessResultWithUserProduct()
        {
            int productId = It.IsAny<int>();
            string productName = "Test1";
            int userId = 1;
            bool isUserAdmin = false;

            List<Product> products = new(){
                new(){
                    UserId = userId,
                    Name = productName,
                    IsDefault = false
                    }
                };

            ProductDto productDto =
                new()
                {
                    Name = productName,
                    IsDefault = false,
                    Id = 4
                };

            _productRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())).Returns(Task.FromResult(products.AsEnumerable()));

            _mapper.Setup(x => x.Map<ProductDto>(products.FirstOrDefault())).Returns(productDto);

            GetProductByIdQueryHandler getProductByIdQueryHandler = new(
                _productRepository.Object,
                _mapper.Object);

            ProductDto result = await getProductByIdQueryHandler.Handle(new GetProductByIdQuery() { IsUserAdmin = isUserAdmin, UserId = userId, ProductId = productId }, CancellationToken.None);

            Assert.AreEqual(4, result.Id);
            Assert.AreEqual(productName, result.Name);
        }

        [Test]
        public async Task GetProductByIdQuery_WhenValidUserHasAdminRole_ReturnsSuccessResultWithAnyUserProduct()
        {
            int productId = It.IsAny<int>();
            string productName = "Test1";
            int userId = 1;

            Product product = new()
            {
                UserId = It.Is<int>(x => x != userId),
                Name = productName,
                IsDefault = false
            };

            ProductDto productDto =
                new()
                {
                    Name = productName,
                    IsDefault = false
                };

            _productRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(product));

            _mapper.Setup(x => x.Map<ProductDto>(product)).Returns(productDto);

            GetProductByIdQueryHandler getProductByIdQueryHandler = new(
                _productRepository.Object,
                _mapper.Object
                );

            var result = await getProductByIdQueryHandler.Handle(new GetProductByIdQuery() { UserId = userId, IsUserAdmin = true, ProductId = productId }, CancellationToken.None);

            Assert.AreEqual(productName, result.Name);
        }

        [Test]
        public async Task GetProductByIdQueryHandler_WhenInvalidIdUserHasAdminRole_ReturnsNotFoundResult()
        {
            int productId = It.IsAny<int>();
            int userId = 1;
            bool isUserAdmin = true;

            _productRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult((Product)null));


            GetProductByIdQueryHandler getProductByIdQueryHandler = new(
                _productRepository.Object,
                _mapper.Object);

            Assert.ThrowsAsync<KeyNotFoundException>(() => getProductByIdQueryHandler.Handle(new GetProductByIdQuery() { UserId = userId, IsUserAdmin = isUserAdmin, ProductId = productId }, CancellationToken.None));
        }
    }
}
