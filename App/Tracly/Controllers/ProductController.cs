using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using App.Tracly.Models;
using System.Collections.Generic;
using API.Web.Entities;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System;
using API.Web.Dtos;

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
            var products = new List<ProductDto>();

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5005/api/products");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ProductDto>>(apiResponse);
                }
            }
            return View("List", products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductsList(string queryString)
        {
            var products = new List<ProductDto>();
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
                    products = JsonConvert.DeserializeObject<List<ProductDto>>(apiResponse);
                }
            }
            return PartialView("_ProductsList", products);
        }

        [HttpGet]
        [Route("Product/GetProduct/{productId:int}")]
        public async Task<ProductDto> GetProduct(int productId)
        {
            var product = new ProductDto();
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5005/api/product/{productId}");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<ProductDto>(apiResponse);
                }
            }
            return product;
        }

        [HttpPost]
        [Route("Product/ProductNameValid")]
        public async Task<bool> ProductNameValid(int productId,string productName)
        {
            bool nameIsValid= false;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5005/api/product/{productId}/nameValid?productName={productName}");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    nameIsValid = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }
            return nameIsValid;
        }

        [HttpPost]
        public async void PostProduct(ProductDto product)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync("http://localhost:5005/api/products", stringContent);
            }
        }

        [HttpPut]
        public async void PutProduct(ProductDto product)
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