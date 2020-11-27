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
using System;

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

        [HttpGet]
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
            return View("List", productListVM.Products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductsList(string queryString)
        {
            var products = new List<Product>();
            var getPath = "http://localhost:5005/api/products";
            var getByNamePath = "http://localhost:5005/api/products/name";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response;
                if (!string.IsNullOrWhiteSpace(queryString))
                {
                    var builder = new UriBuilder(getByNamePath);
                    builder.Query = $"productName={queryString}";
                    response = await httpClient.GetAsync(builder.ToString());
                }
                else
                {
                    response = await httpClient.GetAsync(getPath);
                }
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return PartialView("_ProductListItem", products);
        }

        [HttpGet]
        [Route("Product/GetProduct/{productId:int}")]
        public async Task<Product> GetProduct(int productId)
        {
            var product = new Product();
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5005/api/products/id/{productId}");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return product;
        }

        [HttpPost]
        [Route("Product/ProductNameValid")]
        public async Task<bool> ProductNameValid(string productName)
        {
            bool productNameValid= false;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5005/api/products/exist?productName={productName}");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bool productExist = JsonConvert.DeserializeObject<bool>(apiResponse);
                    productNameValid = !productExist;
                }
            }
            return productNameValid;
        }

        [HttpPost]
        public async void PostProduct(Product product)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync("http://localhost:5005/api/products", stringContent);
            }
        }

        [HttpPut]
        public async void PutProduct(Product product)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PutAsync("http://localhost:5005/api/products", stringContent);
            }
        }

        [HttpDelete]
        [Route("Product/DeleteProduct/{productId:int}")]
        public async void DeleteProduct(int productId)
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"http://localhost:5005/api/products?id={productId}");
            }
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