using System.Collections.Generic;

namespace Tracly.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts {get;}
        Product ProductById(int productId);
    }
}