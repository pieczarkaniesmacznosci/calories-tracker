using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Web.Entities
{
    public class Meal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        [Required]
        public int CompositionId {get;set;}
        [Required]
        public int ProductId {get;set;}
        [Required]
        public int UserId {get;set;}
        [Required]
        public double Weight {get;set;}
        [Required]
        public DateTime Date {get;set;}
    }
}