using AuthenticationAPI.Extensions;
using CaloriesAPI.Dtos;
using CaloriesAPI.Mediator.Commands;
using CaloriesAPI.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private int _userId => Request.HttpContext.User.GetLoggedInUserSub();

        public ProductsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetProducts()
        {
            GetProductsQuery query = new() { UserId = _userId, IsUserAdmin = true };
            IEnumerable<ProductDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("product/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            GetProductByIdQuery query = new() { ProductId = id, UserId = _userId, IsUserAdmin = true };
            ProductDto result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("products/{name}")]
        public async Task<IActionResult> FindProductsByName(string name)
        {
            GetProductByNameQuery query = new() { ProductName = name, UserId = _userId, IsUserAdmin = true };
            IEnumerable<ProductDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> CreateProduct(ProductDto product)
        {
            CreateProductCommand command = new() { UserId = _userId, IsUserAdmin = true, Product = product };
            await _mediator.Send(command);

            // TODO: Change to Created response
            return Ok();
        }

        [HttpPut]
        [Route("product/{id}")]
        public async Task<IActionResult> EditProduct(int id, ProductDto product)
        {
            EditProductCommand command = new() { UserId = _userId, IsUserAdmin = true, ProductId = id, Product = product };
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        [Route("product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            DeleteProductCommand command = new() { UserId = _userId, IsUserAdmin = true, ProductId = id };
            await _mediator.Send(command);

            return Ok();
        }
    }
}
