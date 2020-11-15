using System;
using API.Web.Service;
using API.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Web.Controllers
{
    [ApiController]
    [Route("api/products")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : BaseController
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _service;

        public ProductsController(ILogger<ProductsController> logger, IProductService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service;
        }

        [HttpGet]
        [Route("id/{id}")]
        public IActionResult GetProduct(int id)
        {
            return base.FromResult(_service.GetProduct(id));
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return base.FromResult(_service.GetProducts());
        }

        [HttpGet]
        [Route("name")]
        public IActionResult FindProductByName(string productName)
        {
            return base.FromResult(_service.GetProducts(productName));
        }

        [HttpPost]
        public IActionResult AddProduct(ProductDto product)
        {
            return base.FromResult(_service.AddProduct(product));
        }

        [HttpPut]
        public IActionResult EditProduct(ProductDto product)
        {
            return base.FromResult(_service.EditProduct(product));
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            return base.FromResult(_service.DeleteProduct(id));
        }
    }
}
