using System.Collections.Generic;
using API.Web.Dtos;
using API.Web.Result;

namespace API.Web.Service
{
    public interface IProductService
    {
        Result<ProductDto> GetProduct(int id);
        Result<IEnumerable<ProductDto>> GetProducts();
        Result<IEnumerable<ProductDto>> GetProducts(string productName);
        Result<ProductDto> AddProduct(ProductDto product);
        Result<ProductDto> EditProduct(ProductDto product);
        Result<ProductDto> DeleteProduct(int id);
        Result<bool> ProductExist(string productName);
    }
}