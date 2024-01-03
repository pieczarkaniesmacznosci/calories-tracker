using API.Dtos;
using API.Mediator.Commands;
using API.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/")]
    //[Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetProducts()
        {
            GetProductsQuery query = new() { UserId = 1, IsUserAdmin = true };
            IEnumerable<ProductDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("product/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            GetProductByIdQuery query = new() { ProductId = id, UserId = 1, IsUserAdmin = true };
            ProductDto result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("products/{name}")]
        public async Task<IActionResult> FindProductsByName(string name)
        {
            GetProductByNameQuery query = new() { ProductName = name, UserId = 1, IsUserAdmin = true };
            IEnumerable<ProductDto> result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> CreateProduct(ProductDto product)
        {
            CreateProductCommand command = new() { UserId = 1, IsUserAdmin = true, Product = product };
            await _mediator.Send(command);

            // TODO: Change to Created response
            return Ok();
        }

        [HttpPut]
        [Route("product/{id}")]
        public async Task<IActionResult> EditProduct(int id, ProductDto product)
        {
            EditProductCommand command = new() { UserId = 1, IsUserAdmin = true, ProductId = id, Product = product };
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        [Route("product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            DeleteProductCommand command = new() { UserId = 1, IsUserAdmin = true, ProductId = id };
            await _mediator.Send(command);

            return Ok();
        }
    }
}
