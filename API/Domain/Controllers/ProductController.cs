using System.Collections.Generic;
using API.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
            return new List<ProductDto>(){
                new ProductDto{
                    Kcal = 101d,
                    Protein = 101d,
                    Fat = 3d,
                    Carbohydrates = 2d
                }
            };
        }
    }
}
