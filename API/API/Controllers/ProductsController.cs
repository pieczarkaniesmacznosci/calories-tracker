using API.Dtos;
using API.Identity;
using API.Mediator.Commands;
using API.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IUserManager _userManager;
        private readonly IMediator _mediator;
        private int _userId => _userManager.CurrentUserId;
        private bool _isUserAdmin => _userManager.IsCurrentUserAdmin;

        public ProductsController(
            ILogger<ProductsController> logger,
            IUserManager userManager,
            IMediator mediator)
        {
            _logger = logger;
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("product/{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            GetProductByIdQuery query = new() { ProductId = id, UserId = _userId, IsUserAdmin = _isUserAdmin };
            ProductDto result = await _mediator.Send(query);
            if (result == null)
            {
                _logger.LogInformation("Product with id= {id} was not found!", id);
                return NotFound(id);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("products/{name}")]
        public async Task<IActionResult> FindProductByNameAsync(string name)
        {
            GetProductByNameQuery query = new() { ProductName = name, UserId = _userId, IsUserAdmin = _isUserAdmin };
            IEnumerable<ProductDto> result = await _mediator.Send(query);
            if (result == null)
            {
                _logger.LogInformation("Product with name= {productName} not found!", name);
                return NotFound(name);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetProducts()
        {
            GetProductsQuery query = new() { UserId = _userId, IsUserAdmin = _isUserAdmin };
            IEnumerable<ProductDto> result = await _mediator.Send(query);
            if (result == null)
            {
                _logger.LogInformation("Product not found!");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> CreateProduct(ProductDto product)
        {
            CreateProductCommand command = new() { UserId = _userId, IsUserAdmin = _isUserAdmin, Product = product };
            await _mediator.Send(command);

            // TODO: Change to Created response
            return Ok();
        }

        [HttpPut]
        [Route("product/{id}")]
        public async Task<IActionResult> EditProduct(int id, ProductDto product)
        {
            EditProductCommand command = new() { UserId = _userId, IsUserAdmin = _isUserAdmin, ProductId = id, Product = product };
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        [Route("product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            DeleteProductCommand command = new() { UserId = _userId, IsUserAdmin = _isUserAdmin, ProductId = id };
            await _mediator.Send(command);

            return Ok();
        }
    }
}
