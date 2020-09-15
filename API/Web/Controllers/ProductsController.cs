using System;
using API.Web.Entities;
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
        private readonly IRepository<Product> _productRepository;

        public ProductsController(ILogger<ProductsController> logger, IRepository<Product> productRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productRepository.All());
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var productToReturn = _productRepository.Get(id);

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
