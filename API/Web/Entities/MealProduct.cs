using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Web.Entities
{
    public class MealProduct
    {
        public int Id {get;set;}

        public int ProductId {get;set;}
        public Product Product {get;set;}

        public int MealId {get;set;}
        public Meal Meal {get;set;}

        public double Weight {get;set;}
    }
}