using System;
using System.Collections.Generic;
using System.Linq;

namespace Tracly.Models
{
    public class MockMealRepository : IMealRepository
    {
        public IEnumerable<Meal> AllMeals =>
            new List<Meal>(){
                new Meal(){
                    IsSaved = false,
                    DateEaten =new DateTime(2020,10,19,7,42,0),
                    MealProducts = new List<MealProduct>(){
                        new MealProduct(){
                            Weight = 200,
                            Product = new Product(){
                                Name = "Chicken",
                                Kcal = 250,
                                Protein = 21,
                                Carbohydrates = 1,
                                Fat = 0
                            }
                        },
                        new MealProduct(){
                            Weight = 60,
                            Product = new Product(){
                                Name = "Rice",
                                Kcal = 350,
                                Protein = 5,
                                Carbohydrates = 31,
                                Fat = 0
                            }
                        }
                    }
                }
            };

        public Meal MealById(int mealId) => 
        this.AllMeals.FirstOrDefault(x=>x.Id ==mealId);
    }
}