using System;
using API.Web.Service;
using API.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Web.Controllers;

namespace Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : BaseController
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IService _service;

        public ProductsController(ILogger<ProductsController> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return base.FromResult(_service.GetProducts());
        }
        
        [HttpGet]
        [Route("id/{id}")]
        public IActionResult GetProduct(int id)
        {
            return base.FromResult(_service.GetProduct(id));
        }
        
        [HttpGet]
        [Route("name/{productName}")]
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
