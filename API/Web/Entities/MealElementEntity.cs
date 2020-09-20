using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Web.Entities
{
    public class MealElement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        [ForeignKey("Product")]
        public int ProductId {get;set;}
        public virtual Product Product {get;set;}
        public double Weight {get;set;}
    }
}