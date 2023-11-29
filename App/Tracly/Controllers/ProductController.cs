using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tracly.Dtos;
using Tracly.Extensions;

namespace Tracly.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private IConfiguration _config { get; }
        private string _apiUrl { get; }

        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            _apiUrl = _config["APIUrl"];
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var products = new List<ProductDto>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.GetAsync($"{_apiUrl}/products");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ProductDto>>(apiResponse);
                }
            }
            return View("List", products);
        }

        [HttpGet]
        public async Task<List<ProductDto>> ProductsList(string queryString)
        {
            var products = new List<ProductDto>();
            var getPath = $"{_apiUrl}/products";
            var getByNamePath = $"{_apiUrl}/products/name";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
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
            return products;
        }

        [HttpPost]
        public IActionResult ProductsListTable(IEnumerable<ProductDto> products)
        {
            products ??= new List<ProductDto>();
            return PartialView("_ProductsList", products);
        }

        [HttpGet]
        [Route("Product/GetProduct/{productId:int}")]
        public async Task<ProductDto> GetProduct(int productId)
        {
            var product = new ProductDto();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.GetAsync($"{_apiUrl}/product/{productId}");
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
        public async Task<bool> ProductNameValid(int productId, string productName)
        {
            bool nameIsValid = false;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.GetAsync($"{_apiUrl}/product/{productId}/nameValid?productName={productName}");
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
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.PostAsync($"{_apiUrl}/product", stringContent);
            }
        }

        [HttpPut]
        public async void PutProduct(ProductDto product)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.PutAsync($"{_apiUrl}/product/{product.Id}", stringContent);
            }
        }

        [HttpDelete]
        [Route("Product/DeleteProduct/{productId:int}")]
        public async void DeleteProduct(int productId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = Request.AddAuthenticationToken();
                HttpResponseMessage response = await httpClient.DeleteAsync($"{_apiUrl}/product/{productId}");
            }
        }
    }
}
