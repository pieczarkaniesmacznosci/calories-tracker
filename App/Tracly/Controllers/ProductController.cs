using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using App.Tracly.Models;
using App.Tracly.ViewModels;
using System.Collections.Generic;
using API.Web.Entities;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace App.Tracly.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productrepository;

        public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productrepository = productRepository;
        }

        public async Task<IActionResult> List()
        {
            var productListVM = new ProductListViewModel();

            productListVM.Title = "Products";
            productListVM.Products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5005/api/products");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productListVM.Products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(productListVM);
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(Product product)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync("http://localhost:5005/api/products", stringContent);
            }
            return View("List");
        }

        public IActionResult Details(int id)
        {
            var product = _productrepository.ProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}