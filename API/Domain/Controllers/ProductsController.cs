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
            _logger = logger;
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
            var productToReturn = _productsDataStore.Products.FirstOrDefault(x => x.Id == id);

            if(productToReturn == null){
                return NotFound();
            }
            return base.Ok(productToReturn);
        }
    }
}
