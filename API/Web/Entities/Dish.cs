using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Web.Entities
{
    public class Dish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public string Name {get;set;}
        public int Portions {get;set;}
        public DateTime DateCreated {get;set;}
        public IEnumerable<MealProduct> MealProducts {get;set;}
    }
}