using API.Dtos;
using API.Identity;
using API.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            GetProductByIdQuery query = new() { Id = id, UserId = _userId, IsUserAdmin = _isUserAdmin };
            ProductDto result = await _mediator.Send(query);
            if (result == null)
            {
                _logger.LogInformation("Product with id= {id} was not found!", id);
                return NotFound(id);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("products")]
        public IActionResult GetProducts()
        {
            return base.FromResult(_service.GetProducts());
        }

        [HttpGet]
        [Route("products/name")]
        public IActionResult FindProductByName(string productName)
        {
            return base.FromResult(_service.GetProducts(productName));
        }

        [HttpGet]
        // {_apiUrl}/product/{productId}/nameValid?productName={productName}
        [Route("product/{id}/nameValid")]
        public IActionResult ProductNameValid(int id, string productName)
        {
            return base.FromResult(_service.ProductNameValid(id, productName));
        }

        [HttpPost]
        [Route("product")]
        public IActionResult AddProduct(ProductDto product)
        {
            return base.FromResult(_service.AddProduct(product));
        }

        [HttpPut]
        [Route("product/{id}")]
        public IActionResult EditProduct(int id, ProductDto product)
        {
            return base.FromResult(_service.EditProduct(id, product));
        }

        [HttpDelete]
        [Route("product/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            return base.FromResult(_service.DeleteProduct(id));
        }
    }
}
