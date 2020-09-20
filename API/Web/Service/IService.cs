using System.Collections.Generic;
using API.Web.Models;
using API.Web.Result;

namespace API.Web.Service
{
    public interface IService
    {
        Result<ProductDto> GetProduct(int id);
        Result<IEnumerable<ProductDto>> GetProducts();
        Result<IEnumerable<ProductDto>> GetProducts(string productName);
        Result<ProductDto> AddProduct(ProductDto product);
        Result<ProductDto> EditProduct(ProductDto product);
        Result<ProductDto> DeleteProduct(int id);
    }
}