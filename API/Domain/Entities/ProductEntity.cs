using System.ComponentModel.DataAnnotations;

namespace API.Domain.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public double Kcal {get;set;}
        [Required]
        public double Protein {get;set;}
        [Required]
        public double Carbohydrates {get;set;}
        [Required]
        public double Fat {get;set;}
    }    
}