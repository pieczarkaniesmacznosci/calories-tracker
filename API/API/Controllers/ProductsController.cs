using API.Dtos;
using API.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : BaseController
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _service;

        public ProductsController(
            ILogger<ProductsController> logger,
            IProductService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service;
        }

        [HttpGet]
        [Route("product/{id}")]
        public IActionResult GetProduct(int id)
        {
            return base.FromResult(_service.GetProduct(id));
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
