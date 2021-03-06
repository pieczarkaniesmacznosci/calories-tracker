using System.Collections.Generic;
using System.Linq;
using API.Web.Entities;

namespace App.Tracly.Models
{
    public class MockProductRepository : IProductRepository
    {
        public IEnumerable<Product> AllProducts =>
                 new List<Product>(){
                            new Product()
                            {
                                Id = 1,
                                Name = "Chicken",
                                Kcal = 111.0d,
                                Protein = 21.0d,
                                Carbohydrates = 0.0d,
                                Fat = 3.0d,
                            },
                            new Product()
                            {
                                Id = 2,
                                Name = "Rice",
                                Kcal = 339.5d,
                                Protein = 6.7d,
                                Carbohydrates = 76.6d,
                                Fat = 0.7d,
                            },
                            new Product()
                            {
                                Id = 3,
                                Name = "Milky Chocolate",
                                Kcal = 580.8d,
                                Protein = 7.7d,
                                Carbohydrates = 51.1d,
                                Fat = 38.4d,
                            },
                            new Product()
                            {
                                Id = 4,
                                Name = "White bread",
                                Kcal = 237.7d,
                                Protein = 4.0d,
                                Carbohydrates = 52.5d,
                                Fat = 1.3d,
                            }
                };

        public Product ProductById(int productId) =>
        this.AllProducts.FirstOrDefault(x => x.Id == productId);
    }
}