using System.Collections.Generic;
using API.Web.Models;

namespace DataStore
{
    public class ProductsDataStore
    {
        public List<ProductModel> Products => new List<ProductModel>(){
            new ProductModel(){
                Id = 1,
                Name = "Chicken",
                Kcal = 111.0d,
                Protein = 21.0d,
                Carbohydrates = 0.0d,
                Fat = 3.0d,
            },
            new ProductModel(){
                Id = 2,
                Name = "Rice",
                Kcal = 339.5d,
                Protein = 6.7d,
                Carbohydrates = 76.6d,
                Fat = 0.7d,
            },
            new ProductModel(){
                Id = 3,
                Name = "Milky Chocolate",
                Kcal = 580.8d,
                Protein = 7.7d,
                Carbohydrates = 51.1d,
                Fat = 38.4d,
            },
            new ProductModel(){
                Id = 4,
                Name = "White bread",
                Kcal = 237.7d,
                Protein = 4.0d,
                Carbohydrates = 52.5d,
                Fat = 1.3d,
            }
        };
    }
}