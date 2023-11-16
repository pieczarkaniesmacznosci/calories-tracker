using API.Dtos;
using API.Result;
using System.Collections.Generic;

namespace API.Service
{
    public interface IProductService
    {
        Result<ProductDto> GetProduct(int id);
        Result<IEnumerable<ProductDto>> GetProducts();
        Result<IEnumerable<ProductDto>> GetProducts(string productName);
        Result<ProductDto> AddProduct(ProductDto product);
        Result<ProductDto> EditProduct(int id, ProductDto product);
        Result<ProductDto> DeleteProduct(int id);
        Result<bool> ProductNameValid(int id, string productName);
    }
}
