using System;
using System.Linq;
using API.Web.Entities;
using API.Web.Service;
using API.Web.Models;
using API.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IService _logic;

        public ProductsController(ILogger<ProductsController> logger, IService logic)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logic = logic;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_logic.GetProducts());
        }
        
        [HttpGet]
        [Route("id/{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var productToReturn = _logic.GetProduct(id);

                if(productToReturn == null)
                {
                    _logger.LogInformation($"Product with id = {id} was not found!");
                    return NotFound();
                }
                return base.Ok(productToReturn);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting product with id = {id}",ex);
                return StatusCode(500, "Error while handling the request");
            }
        }
        
        [HttpGet]
        [Route("name/{productName}")]
        public IActionResult FindProductByName(string productName)
        {
            try
            {
                 var productToReturn = _logic.GetProducts(productName);
                if(productToReturn == null)
                {
                    _logger.LogInformation($"Product with name = {productName} was not found!");
                    return NotFound();
                }
                return base.Ok(productToReturn);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while adding product with name = {productName}",ex);
                return StatusCode(500, "Error while handling the request");
            }
        }

        [HttpPost]
        public IActionResult AddProduct(ProductModel product)
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
        public IActionResult EditProduct(ProductModel product)
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
