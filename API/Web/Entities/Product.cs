using System;
using System.Collections.Generic;

namespace API.Web.Entities
{
    public class Product : Entity
    {
        public string Name {get;set;}
        public double Kcal {get;set;}
        public double Protein {get;set;}
        public double Carbohydrates {get;set;}
        public double Fat {get;set;}
        public bool IsAvailable {get;set;}
        public DateTime DateAdded {get;set;}
        public bool IsDefault {get;set;}
        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<MealProduct> MealProducts {get;set;}
    }    
}