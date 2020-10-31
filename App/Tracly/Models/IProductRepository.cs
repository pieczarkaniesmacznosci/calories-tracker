using System.Collections.Generic;

namespace App.Tracly.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Product ProductById(int productId);
    }
}