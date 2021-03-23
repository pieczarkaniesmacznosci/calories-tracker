using System.Collections.Generic;
using API.Entities;

namespace App.Tracly.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Product ProductById(int productId);
    }
}