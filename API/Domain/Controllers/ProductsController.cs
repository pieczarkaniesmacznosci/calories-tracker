using System.Collections.Generic;
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
        public IEnumerable<ProductDto> Get()
        {
            return _productsDataStore.Products;
        }
    }
}
