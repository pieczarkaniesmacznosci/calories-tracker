using System;
using System.Collections.Generic;

namespace Tracly.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public bool IsSaved {get;set;}
        public string MealName {get;set;}
        public double Weight { get; set; }
        public DateTime DateEaten { get; set; }
        public IEnumerable<MealProduct> MealProducts {get;set;}
    }
}