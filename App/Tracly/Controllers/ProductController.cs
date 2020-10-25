using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tracly.Models;
using Tracly.ViewModels;

namespace Tracly.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productrepository;

        public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productrepository = productRepository;
        }

        public IActionResult List()
        {
            var productListVM = new ProductListViewModel();

            productListVM.Title = "Products";
            productListVM.Products = _productrepository.AllProducts;

            return View(productListVM);
        }

        public IActionResult Details(int id)
        {
            var product = _productrepository.ProductById(id);

            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}