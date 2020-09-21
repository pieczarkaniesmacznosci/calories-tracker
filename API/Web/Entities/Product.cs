using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Web.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public string Name {get;set;}
        public double Kcal {get;set;}
        public double Protein {get;set;}
        public double Carbohydrates {get;set;}
        public double Fat {get;set;}

        public IEnumerable<MealProduct> MealProducts {get;set;}
    }    
}