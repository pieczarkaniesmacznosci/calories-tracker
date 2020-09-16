using System.Collections.Generic;
using API.Web.Entities;
using API.Web.Models;
using API.Web.Result;

namespace API.Web.Service
{
    public interface IService
    {
        Result<IEnumerable<ProductDto>> GetProducts();
        Product GetProduct(int id);
        IEnumerable<Product> GetProducts(string productName);
        Product AddProduct(ProductDto product);
        Product EditProduct(ProductDto product);
        Product DeleteProduct(int id);
    }
}