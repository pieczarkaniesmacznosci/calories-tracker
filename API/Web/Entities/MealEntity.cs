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
        [Required]
        public int MealElamentId {get;set;}
        [Required]
        public IEnumerable<MealElementEntity> MealElements {get;set;}
        [Required]
        public double Weight {get => MealElements.Sum(x=>x.Weight);}
        [Required]
        public DateTime Date {get;set;}
    }
}