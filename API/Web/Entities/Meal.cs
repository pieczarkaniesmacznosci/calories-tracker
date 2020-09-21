using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace API.Web.Entities
{
    public class Meal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public double Weight => MealProducts.Sum(x=>x.Weight);
        public DateTime Date {get;set;}

        public IEnumerable<MealProduct> MealProducts {get;set;}
    }
}