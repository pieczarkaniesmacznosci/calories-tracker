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
        //public int MealElementsId {get;set;}
        public IEnumerable<MealElement> MealElements {get;set;}
        public double Weight {get;set;}// => MealElements.Sum(x=>x.Weight);}
        [Required]
        public DateTime Date {get;set;}
    }
}