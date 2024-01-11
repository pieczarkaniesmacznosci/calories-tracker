using AutoMapper;
using CaloriesAPI.Dtos;
using CaloriesAPI.Mediator.Handlers;
using CaloriesAPI.Mediator.Queries;
using Entities;
using Moq;
using NUnit.Framework;
using Repositories;
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
        private readonly Guid _userGuid = new("99000000-0000-0000-0000-000000000000");
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
            List<Product> products = new(){
                new(){
                    UserId = _userGuid,
                    IsDefault = false
                },
                new(){
                    UserId = _userGuid,
                    IsDefault = false
                },
                new(){
                    UserId = _userGuid,
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

            Assert.That(3, Is.EqualTo(result.Count()));
        }

        [Test]
        public async Task GetProductByIdQueryHandler_WhenValidUserHasUserRole_ReturnsSuccessResultWithUserProduct()
        {
            int productId = It.IsAny<int>();
            string productName = "Test1";
            bool isUserAdmin = false;

            List<Product> products = new(){
                new(){
                    UserId = _userGuid,
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

            ProductDto result = await getProductByIdQueryHandler.Handle(new GetProductByIdQuery() { IsUserAdmin = isUserAdmin, UserId = _userGuid, ProductId = productId }, CancellationToken.None);

            Assert.That(4, Is.EqualTo(result.Id));
            Assert.That(productName, Is.EqualTo(result.Name));
        }

        [Test]
        public async Task GetProductByIdQuery_WhenValidUserHasAdminRole_ReturnsSuccessResultWithAnyUserProduct()
        {
            int productId = It.IsAny<int>();
            string productName = "Test1";

            Product product = new()
            {
                UserId = It.Is<Guid>(x => x != _userGuid),
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

            var result = await getProductByIdQueryHandler.Handle(new GetProductByIdQuery() { UserId = _userGuid, IsUserAdmin = true, ProductId = productId }, CancellationToken.None);

            Assert.That(productName, Is.EqualTo(result.Name));
        }

        [Test]
        public void GetProductByIdQueryHandler_WhenInvalidIdUserHasAdminRole_ReturnsNotFoundResult()
        {
            int productId = It.IsAny<int>();
            bool isUserAdmin = true;

            _productRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult((Product)null));

            GetProductByIdQueryHandler getProductByIdQueryHandler = new(
                _productRepository.Object,
                _mapper.Object);

            Assert.ThrowsAsync<KeyNotFoundException>(() => getProductByIdQueryHandler.Handle(new GetProductByIdQuery() { UserId = _userGuid, IsUserAdmin = isUserAdmin, ProductId = productId }, CancellationToken.None));
        }
    }
}
