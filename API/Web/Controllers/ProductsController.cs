using System;
using System.Linq;
using API.Web.Entities;
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
        [Route("id/{id}")]
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
        
        [HttpGet]
        [Route("name/{productName}")]
        public IActionResult FindProductByName(string productName)
        {
            try
            {
                 var productToReturn = _productRepository.Find(x=>x.Name.Contains(productName));
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
                var productEntity = new Product{
                    Name = product.Name,
                    Kcal = product.Kcal,
                    Protein = product.Protein,
                    Carbohydrates= product.Carbohydrates,
                    Fat = product.Fat
                };

                var productToReturn = _productRepository.Add(productEntity);
                _productRepository.SaveChanges();
                if(productToReturn == null)
                {
                    _logger.LogInformation($"Product with id = {product.Name} was not found!");
                    return NotFound();
                }
                return base.Ok(productToReturn);
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
                 var productToReturn = _productRepository.Get(product.Id);
                if(productToReturn == null)
                {
                    _logger.LogInformation($"Product with id = {product.Id} was not found!");
                    return NotFound();
                }

                var productEntity = new Product{
                    Id = product.Id,
                    Name = product.Name,
                    Kcal = product.Kcal,
                    Protein = product.Protein,
                    Carbohydrates= product.Carbohydrates,
                    Fat = product.Fat
                };
                
                productToReturn = _productRepository.Update(productEntity);
                _productRepository.SaveChanges();
                return base.Ok(productToReturn);
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
                var productToReturn = _productRepository.Find(x=>x.Id == id).SingleOrDefault();
                if(productToReturn == null)
                {
                    _logger.LogInformation($"Product with id = {id} was not found!");
                    return NotFound();
                }
                
                _productRepository.Delete(productToReturn);
                _productRepository.SaveChanges();

                return base.Ok(productToReturn);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while adding product with name = {id}",ex);
                return StatusCode(500, "Error while handling the request");
            }
        }
    }
}
