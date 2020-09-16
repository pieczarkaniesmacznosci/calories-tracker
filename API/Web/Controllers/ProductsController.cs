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
            try
            {
                var result = _logic.GetProducts(productName);
                return this.FromResult<ProductDto>(result);
               
                // return base.Ok(productToReturn);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while adding product with name = {productName}",ex);
                return StatusCode(500, "Error while handling the request");
            }
        }

        [HttpPost]
        public IActionResult AddProduct(ProductDto product)
        {
            try
            {
                var result = _logic.AddProduct(product);
                return base.Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while adding product with name = {product.Name}",ex);
                return StatusCode(500, "Error while handling the request");
            }
        }

        [HttpPut]
        public IActionResult EditProduct(ProductDto product)
        {
            try
            {
                // var productToReturn = _productRepository.Get(product.Id);
                // if(productToReturn == null)
                // {
                //     _logger.LogInformation($"Product with id = {product.Id} was not found!");
                //     return NotFound();
                // }

                var result = _logic.EditProduct(product);
                return base.Ok(result);

            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while adding product with name = {product.Name}",ex);
                return StatusCode(500, "Error while handling the request");
            }
        }
        
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var result = _logic.DeleteProduct(id);
                return base.Ok(result);
                // if(productToReturn == null)
                // {
                //     _logger.LogInformation($"Product with id = {id} was not found!");
                //     return NotFound();
                // }
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while adding product with name = {id}",ex);
                return StatusCode(500, "Error while handling the request");
            }
        }
    }
}
