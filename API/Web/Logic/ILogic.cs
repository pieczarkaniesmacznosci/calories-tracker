using System.Collections.Generic;
using API.Web.Entities;
using API.Web.Models;

namespace API.Web.Logic
{
    public interface ILogic
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        IEnumerable<Product> GetProducts(string productName);
        Product AddProduct(ProductModel product);
        Product EditProduct(ProductModel product);
        Product DeleteProduct(int id);
    }
}