using System.ComponentModel.DataAnnotations;

namespace API.Domain.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id {get;set;}
        [Required]
        public string Name {get;set;}
        public double Kcal {get;set;}
        public double Protein {get;set;}
        public double Carbohydrates {get;set;}
        public double Fat {get;set;}
    }    
}