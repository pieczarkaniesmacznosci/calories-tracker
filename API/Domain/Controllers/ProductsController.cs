using System;
using System.Collections.Generic;
using System.Linq;
using API.Domain.Models;
using DataStore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        private ProductsDataStore _productsDataStore { get; }

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productsDataStore = new ProductsDataStore();
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productsDataStore.Products);
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var productToReturn = _productsDataStore.Products.FirstOrDefault(x => x.Id == id);

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
    }
}
