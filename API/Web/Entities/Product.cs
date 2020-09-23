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

        public IEnumerable<MealProduct> MealProducts {get;set;}
    }    
}