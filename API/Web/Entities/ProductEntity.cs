using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Web.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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